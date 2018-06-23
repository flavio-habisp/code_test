using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WebMVC.Models;
using PagedList;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? page, string searchString)
        {
            IEnumerable<MockData> mockDatas = SearchMockData(searchString);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.searchString = !String.IsNullOrEmpty(searchString) ? searchString : string.Empty;
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            return View(mockDatas.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult List(int? page, string searchString)
        {
            IEnumerable<MockData> mockDatas = SearchMockData(searchString);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.searchString = !String.IsNullOrEmpty(searchString) ? searchString : string.Empty;
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            return View(mockDatas.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            MockData mockData = new MockData();
            if (id != 0)
            {
                MockDataContext mockDataContext = new MockDataContext();
                mockData = mockDataContext.MockDatas.Single(mock => mock.MockDataId == id);

            }
            return View(mockData);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdate(MockData mockData)
        {
            try
            {
                MockDataContext mockDataContext = new MockDataContext();

                if (mockData.MockDataId != 0)
                {
                    mockDataContext.MockDatas.Attach(mockData);
                    mockDataContext.Entry(mockData).State = EntityState.Modified;
                }
                else
                {
                    mockDataContext.MockDatas.Add(mockData);
                }

                mockDataContext.SaveChanges();

                return Json(new { success = true, message = "Saved Successfully!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) { 

                return Json(new { success = false, message = "Error! " + ex.Message  }, JsonRequestBehavior.AllowGet);

            }
        }
        
        public ActionResult Delete(int id)
        {
            MockData mockData = new MockData();
            try
            {
                MockDataContext mockDataContext = new MockDataContext();
                mockData = mockDataContext.MockDatas.Single(mock => mock.MockDataId == id);
                mockDataContext.Entry(mockData).State = EntityState.Deleted;
                mockDataContext.SaveChanges();

                return Json(new { success = true, message = "Deleted Successfully!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = "Error! " + ex.Message }, JsonRequestBehavior.AllowGet);

            }
        }


        private static IEnumerable<MockData> SearchMockData(string searchString)
        {
            MockDataContext mockDataContext = new MockDataContext();
            List<MockData> mockDatasList = mockDataContext.MockDatas.ToList();

            var mockDatas = from s in mockDatasList
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                mockDatas = mockDatas.Where(s => (s.FirstName ?? string.Empty).ToLower().Contains(searchString.ToLower())
                                            || (s.LastName ?? string.Empty).ToLower().Contains(searchString.ToLower())
                                            || (s.Email ?? string.Empty).ToLower().Contains(searchString.ToLower())
                                            );
            }

            return mockDatas;
        }

    }
}