using System;
using System.Linq;
using System.Windows.Forms;
using BeverageManagement.Forms.Reconcile;
using Jaxis.DrinkInventory.BeverageManagement.UnitTest.Fakes;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using NUnit.Framework;

namespace Jaxis.DrinkInventory.BeverageManagement.UnitTest.BeverageManagementTest
{
    [TestFixture]
    public class TicketItemAliasesPresenterTest
    {
        private IBLManagerFactory m_factory;
        private FakeTicketItemAliasesView m_view;

        [SetUp]
        public void SetUp()
        {
            m_factory = FakeManagerFactory.Recreate();
        }

        private void CreateView()
        {
            m_view = new FakeTicketItemAliasesView(m_factory);
            m_view.FireLoad();
        }

        [Test]
        public void ListsTicketItems()
        {
            CreateAlias("Alias 1");
            CreateAlias("Alias 2");
            CreateView();
            Assert.AreEqual(2, m_view.Aliases.ToList().Count);
        }

        [Test]
        public void PopulatesRecipeList()
        {
            CreateRecipe("Chalk");
            CreateRecipe("Eraser");
            CreateView();
            Assert.AreEqual(3, m_view.RecipeList.ToList().Count);
        }

        [Test]
        public void OkSaves()
        {
            CreateView();
            m_view.ConcreteAliases.Add(new TicketItemAliasDisplay { DescriptionOnTicket = "BR-549" });
            m_view.Close(DialogResult.OK);
            var alias = (from a in m_factory.ManageTicketItemAliases().GetAll() where a.Description == "BR-549" select a).FirstOrDefault();
            Assert.IsNotNull(alias);
            Assert.IsTrue(m_view.Closed);
        }

        [Test]
        public void CancelDoesNotSave()
        {
            CreateView();
            m_view.ConcreteAliases.Add(new TicketItemAliasDisplay { DescriptionOnTicket = "BR-549" });
            m_view.Close(DialogResult.Cancel);
            var alias = (from a in m_factory.ManageTicketItemAliases().GetAll() where a.Description == "BR-549" select a).FirstOrDefault();
            Assert.IsNull(alias);
            Assert.IsTrue(m_view.Closed);
        }

        [Test]
        public void ShowsUnknownItems()
        {
            CreateTicket();
            CreateView();
            Assert.AreEqual(2, m_view.UnknownTicketItems.ToList().Count);
        }

        [Test]
        public void DeletesAlias()
        {
            CreateAlias("Alias 1");
            CreateAlias("Alias 2");
            CreateView();
            m_view.SelectedAlias = (from a in m_view.ConcreteAliases where a.DescriptionOnTicket == "Alias 2" select a).FirstOrDefault();
            m_view.FireDeleteClick();
            Assert.IsNotNull((from a in m_view.ConcreteAliases where a.DescriptionOnTicket == "Alias 1" select a).FirstOrDefault());
            Assert.IsNull((from a in m_view.ConcreteAliases where a.DescriptionOnTicket == "Alias 2" select a).FirstOrDefault());
        }

        [Test]
        public void AssisgnUnknownItem()
        {
            CreateTicket();
            CreateView();
            m_view.SelectedUnknownItem = "Item 2";
            m_view.FireAssignClick();
            Assert.AreEqual(1, m_view.UnknownTicketItems.ToList().Count);
            Assert.IsNull((from u in m_view.UnknownTicketItems where u == "Item 2" select u).FirstOrDefault());
            Assert.IsNotNull((from a in m_view.Aliases where a.DescriptionOnTicket == "Item 2" select a).FirstOrDefault());
        }

        [Test]
        public void DeletePutsUnknownBack()
        {
            CreateTicket();
            CreateAlias("Item 1");
            CreateAlias("Item 2");
            CreateView();
            m_view.SelectedAlias = (from a in m_view.Aliases where a.DescriptionOnTicket == "Item 2" select a).FirstOrDefault();
            Assert.IsNull((from u in m_view.UnknownTicketItems where u == "Item 2" select u).FirstOrDefault());
            m_view.FireDeleteClick();
            Assert.IsNotNull((from u in m_view.UnknownTicketItems where u == "Item 2" select u).FirstOrDefault());
        }

        private void CreateTicket()
        {
            var ticketManager = m_factory.ManagePOSTickets();
            var ticket = ticketManager.Create();
            ticket.CheckNumber = "101";
            ticket.Comments = "Comment";
            ticket.CustomerTable = "Table";
            ticket.Establishment = "Establishment";
            ticket.GuestCount = 42;
            ticket.TicketDate = new DateTime(2010, 1, 13);
            ticketManager.Save(ticket);
            CreateItem(ticket, "Item 1");
            CreateItem(ticket, "Item 2");
        }

        private void CreateItem(IBLPOSTicket _ticket, string _description)
        {
            var itemManager = m_factory.ManagePOSTicketItems();
            var item = itemManager.Create();
            item.POSTicketID = _ticket.POSTicketID;
            item.Description = _description;
            itemManager.Save(item);
        }

        private void CreateRecipe(string _description)
        {
            var recipe = m_factory.ManageRecipes().Create();
            recipe.Description = _description;
            m_factory.ManageRecipes().Save(recipe);
        }

        private void CreateAlias(string _description)
        {
            var alias = m_factory.ManageTicketItemAliases().Create();
            alias.Description = _description;
            m_factory.ManageTicketItemAliases().Save(alias);
        }
    }
}





