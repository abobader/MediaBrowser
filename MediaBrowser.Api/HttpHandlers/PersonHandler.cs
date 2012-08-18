﻿using System;
using System.Collections.Generic;
using System.Linq;
using MediaBrowser.Common.Net.Handlers;
using MediaBrowser.Controller;
using MediaBrowser.Model.DTO;
using MediaBrowser.Model.Entities;

namespace MediaBrowser.Api.HttpHandlers
{
    /// <summary>
    /// Gets a single Person
    /// </summary>
    public class PersonHandler : BaseJsonHandler<IBNItem<Person>>
    {
        protected override IBNItem<Person> GetObjectToSerialize()
        {
            Folder parent = ApiService.GetItemById(QueryString["id"]) as Folder;
            Guid userId = Guid.Parse(QueryString["userid"]);
            User user = Kernel.Instance.Users.First(u => u.Id == userId);

            string name = QueryString["name"];

            return GetPerson(parent, user, name);
        }

        /// <summary>
        /// Gets a Person
        /// </summary>
        private IBNItem<Person> GetPerson(Folder parent, User user, string name)
        {
            int count = 0;

            // Get all the allowed recursive children
            IEnumerable<BaseItem> allItems = parent.GetParentalAllowedRecursiveChildren(user);

            foreach (var item in allItems)
            {
                if (item.People != null && item.People.Any(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                {
                    count++;
                }
            }

            // Get the original entity so that we can also supply the PrimaryImagePath
            return new IBNItem<Person>()
            {
                Item = Kernel.Instance.ItemController.GetPerson(name),
                BaseItemCount = count
            };
        }
    }
}
