using CategoryService.Core.Interfaces;
using CategoryService.Core.Services;
using CategoryService.Model;
using SharedService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryService.Core
{
    public class CategoryServiceFactory
    {
        private readonly IServiceProvider serviceProvider;

        public CategoryServiceFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public ICategoryService GetCategoryService(string type)
        {
            if (String.Compare(CategoryType.Electronics.ToString(), type, true) == 0)
            {
                return (ICategoryService)serviceProvider.GetService(typeof(EelectronicCategoryService));
            }
            else if (String.Compare(CategoryType.HomeAppliances.ToString(), type, true) == 0)
            {
                return (ICategoryService)serviceProvider.GetService(typeof(HomeApplianceCategoryService));
            }
            else
            {
                return (ICategoryService)serviceProvider.GetService(typeof(CategoryService.Core.Services.CategoryService));
            }
        }
    }
}
