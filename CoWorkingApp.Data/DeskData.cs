using System;
using CoWorking.App.Models;

using System.Linq;
using System.Collections.Generic;
using CoWorking.App.Models.Enumerations;

namespace CoWorkingApp.Data
{
    public class DeskData
    {
        private JsonManager<Desk>  jsonManager;
        public DeskData()
        {
            jsonManager = new JsonManager<Desk>();
        }


        public bool CreateDesk(Desk desk)
        {
            var deskCollection = jsonManager.GetCollection();
            deskCollection.Add(desk);
            jsonManager.SaveCollection(deskCollection);
            return true;
        }

        public bool EditDesk(Desk editDesk)
        {
            var deskCollection = jsonManager.GetCollection();
            var indexDesk = deskCollection.FindIndex(p => p.DeskId == editDesk.DeskId);
            deskCollection[indexDesk] = editDesk;
            jsonManager.SaveCollection(deskCollection);
            return true;
        }
        public bool DeleteDesk(Guid deskId)
        {
            var deskCollection = jsonManager.GetCollection();
            var indexDesk = deskCollection.FindIndex(p => p.DeskId == deskId);
            deskCollection.RemoveAt(indexDesk);
            jsonManager.SaveCollection(deskCollection);
            return true;
        }
         public Desk FinDesk(string numberDesk)
        {
            var deskCollection = jsonManager.GetCollection();
            return deskCollection.FirstOrDefault(p => p.Number == numberDesk);

        }

        public IEnumerable<Desk> GetAvailableDesks(){
            return jsonManager.GetCollection().Where(d => d.DeskStatus == DeskStatus.Active);
        }
       


    }
}