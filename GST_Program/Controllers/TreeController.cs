﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GST_Program.Domain;
using GST_Program.Domain.ViewModels;
using GST_Program.Models;

namespace GST_Program.Controllers {
    public class TreeController : Controller {

        DatabaseModel database = new DatabaseModel();
        List<BadgeReceived> badgeList = new List<BadgeReceived>();

		// GET: Tree
        public ActionResult Index() {
            return View();
        }


		// GET: Tree/GridView
		public ActionResult GridView(String ID) {
            badgeList = database.ReadAllBadgeReceivedByReceiver("10010");

			// ID: used to get Badges received by Person with ID
			return View(badgeList);
		}


		// GET: Tree/GiveBadge
		public ActionResult GiveBadge() {
			DatabaseModel file = new DatabaseModel();
			BadgeBank bb = new BadgeBank();
			bb.badges = file.ReadAllBadge();

			PersonViewModel pvm = new PersonViewModel();
			pvm.people = file.ReadAllPerson();

			ViewBag.Badges = bb;
			ViewBag.People = pvm;

			return View();
		}


		// POST: Tree/GiveBadge
		[HttpPost]
		public ActionResult GiveBadge(BadgeReceived b) {
			DatabaseModel file = new DatabaseModel();
			b.Time_Stamp = DateTime.Now;

			Badge badge = file.ReadSingleBadge(b.Badge_ID);
			Person giver = file.ReadSinglePerson(b.ID_Giver);

			if (ModelState.IsValid) {
				file.Create(b);
				return RedirectToAction("GridView");
			}

			return View(b);
		}
    }
}
