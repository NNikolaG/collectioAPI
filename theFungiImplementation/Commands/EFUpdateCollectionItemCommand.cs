﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theFungiApplication.DataTransfer;
using theFungiApplication.Exceptions;
using theFungiApplication.UseCases.Commands;
using theFungiDataAccess;
using theFungiDomain.Entities;
using theFungiImplementation.Validators;

namespace theFungiImplementation.Commands
{
    public class EFUpdateCollectionItemCommand : IUpdateCollectionItemCommand
    {
        private readonly theFungiDbContext _db;
        private readonly UpdateCollectionItemValidator _validator;

        public EFUpdateCollectionItemCommand(theFungiDbContext db, UpdateCollectionItemValidator validator)
        {
            _validator = validator;
            _db = db;
        }
        public int Id => 27;

        public string Name => "Update Collection Item Command";

        public void Execute(CollectionItemCreateDto request)
        {
            _validator.ValidateAndThrow(request);

            var item = _db.CollectionItems.Find(request.Id);

            if (item == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(CollectionItems));
            }

             item.Title = request.Title;
             item.Image = request.Image;
            item.LastModifiedAt = DateTime.UtcNow;
            if (!string.IsNullOrEmpty(request.Model))
            {
                item.Model = request.Model;
            }

            _db.SaveChanges();
        }
    }
}
