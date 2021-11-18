using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using Business.BusinessAspect.Autofac;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;

namespace Business.Concrete
{
    public class ProductManager:IProductService
    {
        readonly IProductDal _productDal;
        private readonly ICategoryService _categoryService;

        

        public ProductManager(IProductDal productDal , ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 12)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id),Messages.CategoryById);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(),Messages.ProductDetailsListed);
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            var results = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfProductName(product.ProductName), CheckIfCategoryCount());

            if (results!=null)
            {
                return results;
            }
            _productDal.Add(product);
            return new Result(true, Messages.ProductAdded);

           
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var results = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (results >= 10)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        private IResult CheckIfProductName(string productName)
        {
            var results = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (results)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        private IResult CheckIfCategoryCount()
        {
            var results = _categoryService.GetAll().Data.Count;
            if (results > 10)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

    }
}
