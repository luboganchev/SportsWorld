namespace SportsWorld.Web.Areas.Company.Controllers
{
    using SportsWorld.Data;
    using System.Web.Mvc;
    using System.Linq;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using AutoMapper;
    using SportsWorld.Models;
    using System.Collections;
    using System.Collections.Generic;
    using SportsWorld.Web.Areas.Company.Models;

    public class EventController : BaseCompanyController
    {
        public EventController(ISportsWorldData data)
            : base(data)
        {
        }

        public virtual ActionResult Update([DataSourceRequest] DataSourceRequest request, FieldEventViewModel fieldEvent)
        {
            if (ModelState.IsValid)
            {
                var currentEvent = this.data.GameEvents.Find(fieldEvent.ID);
                currentEvent = Mapper.Map<FieldEventViewModel, GameEvent>(fieldEvent, currentEvent);
                this.data.GameEvents.Update(currentEvent);
                this.data.SaveChanges();
            }

            return Json(new[] { fieldEvent }.ToDataSourceResult(request, ModelState));
        }
    }
}