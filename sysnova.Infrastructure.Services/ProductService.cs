﻿using sysnova.Domain.Entities;
using sysnova.Domain.Interfaces;
using sysnova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        // Repositories will be injected
        private IRepository<Category> _categoriesRep;
        private IRepository<Cloud> _cloudRep;
        //private IRepository<HelpDesk> _helpDeskRep;
        //private IRepository<Issues> _issuesRep;
        //private IUnitOfWork _uow;

        public ProductService(IRepository<Category> categoriesRep, IRepository<Cloud> cloudRep) //IRepository<HelpDesk> helpdeskRep, IRepository<Issues> issuesRep) //, IUnitOfWork uow)
        {
            _categoriesRep = categoriesRep;
            _cloudRep = cloudRep;
            //_helpDeskRep = helpdeskRep;
            //_issuesRep = issuesRep;
            //_uow = uow;
        }

        public IEnumerable<Category> GetCategories(int Id)
        {
            // Return all categories
            IEnumerable<Category> categories = _categoriesRep.GetById(Id);
            return categories;
        }

        public void UpdateCat (Category category)
        {
            Category _cat = (Category)_categoriesRep.GetById(category.CategoryId).FirstOrDefault();
            _cat.CategoryName = category.CategoryName;
            _cat.Description = category.Description;
            _categoriesRep.Update(_cat);
        }
        public int AddCloud(Cloud Item)
        {
            //// Return all categories
            int cloudes = 0;
            //int cloudes = _cloudRep.Add(Item);            
            ////_uow.Commit();
            return cloudes;
        }

        public int AddHelpDesk(HelpDesk Item)
        {
            //// Return all categories
            int help = 0;
            //int help = _helpDeskRep.Add(Item);
            ////_uow.Commit();
            return help;
        }

        public IEnumerable<Cloud> GetFormCloud(int Id)
        {
            // Return all categories
            List<Cloud> list = new List<Cloud>();
            IEnumerable<Cloud> cloud = list;

            //IEnumerable<Cloud> cloud = _cloudRep.GetById(Id);
            ////_uow.Commit();
            return cloud;
        }

        public IEnumerable<HelpDesk> GetFormHelpDesk(int Id)
        {
            //// Return all categories
            List<HelpDesk> list = new List<HelpDesk>();
            IEnumerable<HelpDesk> helpDesk = list;

            //IEnumerable<HelpDesk> helpDesk = _helpDeskRep.GetById(Id);
            ////_uow.Commit();
            return helpDesk;
        }

        public IEnumerable<Issues> GetAllIssues()
        {
            //// Return all categories
            List<Issues> list = new List<Issues>();
            IEnumerable<Issues> issues = list;

            //IEnumerable<Issues> issues = _issuesRep.GetAll();
            ////_uow.Commit();
            return issues;
        }

    }
}
