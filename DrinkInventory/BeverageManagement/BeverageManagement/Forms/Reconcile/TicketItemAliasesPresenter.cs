using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;

namespace BeverageManagement.Forms.Reconcile
{
    public class TicketItemAliasesPresenter
    {
        private readonly ITicketItemAliasesView m_view;
        private readonly IBLManagerFactory m_factory;
        private readonly List<string> m_descriptions;

        public TicketItemAliasesPresenter(ITicketItemAliasesView _view, IBLManagerFactory _factory = null)
        {
            m_view = _view;
            m_factory = _factory ?? BLManagerFactory.Get();
            m_descriptions = m_factory.ManagePOSTicketItems().GetUniqueDescriptions().ToList();
            SubscribeViewEvents();
        }

        private void SubscribeViewEvents()
        {
            m_view.Load += Load;
            m_view.Closing += Closing;
            m_view.DeleteClick += DeleteClick;
            m_view.AssignClick += AssignClick;
            m_view.AliasModified += AliasModified;
        }

        private void AliasModified(object _sender, EventArgs _e)
        {
            HandleEvent(RefreshUnknownItems);
        }

        private void RefreshUnknownItems()
        {
            m_view.UnknownTicketItems = BLManagerFactory.Get().ManageTicketItemAliases().GetUnknownAliases();
        }

        private void AssignClick(object _sender, EventArgs _e)
        {
            HandleEvent(() =>
            {
                var unknownItem = m_view.SelectedUnknownItem;
                if (unknownItem == null) return;
                m_view.AddAlias(unknownItem, 0.0m);
                RefreshUnknownItems();
            });
        }

        private void DeleteClick(object _sender, EventArgs _e)
        {
            HandleEvent(() =>
            {
                m_view.DeleteSelectedAlias();
                RefreshUnknownItems();
            });
        }

        private void Closing(object _sender, FormClosingEventArgs _e)
        {
            HandleEvent(() =>
            {
                if (m_view.DialogResult == DialogResult.OK)
                {
                    SaveChanges();
                }
            });
        }

        private void HandleEvent(Action _action)
        {
            try
            {
                _action();
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                m_view.ShowError(string.Format("An error occurred: {0}", ex.Message));
            }
        }

        private void SaveChanges()
        {
            Log.Debug("TicketItemAliasPresenter.SaveChanges called");

            var viewAliases = m_view.Aliases.Where(a => a.Modified == TicketItemAliasStatus.Modified);
            var deleteAliases = m_view.Aliases.Where(a => a.Modified == TicketItemAliasStatus.Deleted);
            var dbAliases = m_factory.ManageTicketItemAliases().GetAll();
            
            // save everything that has been added or modified, right now just doing EVERYTHING
            // really need a flag in the view aliases to track if it needs saved or not
            foreach (var viewAlias in viewAliases)
            {
                var dbAlias = (from a in dbAliases where a.ObjectID == viewAlias.TicketItemAliasId
                    select a).FirstOrDefault() ?? m_factory.ManageTicketItemAliases().Create();
                dbAlias.Description = viewAlias.DescriptionOnTicket;
                dbAlias.RecipeID = viewAlias.AssignedDrinkRecipe;
                dbAlias.Price = viewAlias.Price;
                Log.Debug(string.Format("TicketItemAliasPresenter.SaveChanges saving alias {0}", dbAlias.Description));
                m_factory.ManageTicketItemAliases().Save(dbAlias);
            }

            // delete everything that has been removed from the view
            foreach (var dbAlias in m_view.DeletedAliases)
            {
                var items = dbAliases.Where(a => a.Description == dbAlias);
                foreach (var blTicketItemAliase in items)
                {
                    m_factory.ManageTicketItemAliases().Delete(blTicketItemAliase);
                    Log.Debug(string.Format("TicketItemAliasPresenter.SaveChanges deleting alias {0}", blTicketItemAliase.Description));
                }
            }
        }

        private void Load(object _sender, EventArgs _e)
        {
            var recipes = new List<IBLRecipe> {null};
            recipes.AddRange(m_factory.ManageRecipes().GetAll());
            m_view.RecipeList = recipes;

            m_view.Aliases = from a in m_factory.ManageTicketItemAliases().GetAll() select new TicketItemAliasDisplay(a);
            RefreshUnknownItems();
        }
    }
}
