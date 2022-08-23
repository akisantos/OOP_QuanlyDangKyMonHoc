using LearnASPWithAkichan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LearnASPWithAkichan.Controllers
{
    // [Authorize(Policy = "Student")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly regist_courseContext _db;
        private bool RoleCheckByHand;
        private bool trongKyDangKy;
        public HomeController(ILogger<HomeController> logger, regist_courseContext db)
        {
            _logger = logger;
            _db = db;
           
        }


        public IActionResult Login()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("role");
            HttpContext.Session.Remove("trongKyDangKy");

            return RedirectToAction("Index", "Accounts");
        }
        /*******/
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("role") == "True")
            {
                RoleCheckByHand = true;
            }
            else
            {
                RoleCheckByHand = false;
            }

            if (HttpContext.Session.GetString("trongKyDangKy") == "True")
            {
                trongKyDangKy = true;
            }
            else
            {
                trongKyDangKy = false;
            }

            if (trongKyDangKy && RoleCheckByHand)
            {
                string username = HttpContext.Session.GetString("username");
                var acc = _db.Accounts.FirstOrDefault(x => x.UserName == username);
                var stu = _db.Students.FirstOrDefault(x => x.AccountId == acc.Id);
                HttpContext.Session.SetString("studentID", stu.Id);
                var k = _db.Departments.FirstOrDefault(x => x.Id == stu.DepartmentId);
                var sub = _db.Subjects.FirstOrDefault(x => x.DepartmentId == stu.DepartmentId);
                var listSub = from s in _db.Subjects where s.DepartmentId == k.Id select s;
                TempData["checker"] = "Đăng ký";
                return View(await listSub.ToListAsync());
            }

            TempData["notification"] = "Chưa đến hạn đăng ký";
       
            return View();


        }
        /*******/
        // Xem danh sách lớp học phần đã đăng ký
        public async Task<IActionResult> HocPhanDaDangKy()
        {
            string username = HttpContext.Session.GetString("username");
            var acc = _db.Accounts.FirstOrDefault(x => x.UserName == username);
            var stuID = _db.Students.FirstOrDefault(x => x.AccountId == acc.Id);
            //
            var regTest = _db.RegistClasses.Where(t => t.StudentId == stuID.Id);
            //
            return View(await regTest.ToListAsync());
        }
        //Đăng ký lớp học phần 
        public async Task<IActionResult> add(string id)
        {
            string username = HttpContext.Session.GetString("username");
            var acc = _db.Accounts.FirstOrDefault(x => x.UserName == username);
            ClassSession clas = _db.ClassSessions.FirstOrDefault(x => x.Id == id);
            //
            var sub = _db.Subjects.FirstOrDefault(x => x.Id == clas.SubjectId);
            var stu = _db.Students.FirstOrDefault(x => x.AccountId == acc.Id);
            //
            if (!KiemTraLopDaDangKy(id, stu.Id.ToString()))
            {
                RegistClass rgclass = new RegistClass()
                {
                    StudentId = stu.Id,
                    ClassSessionId = clas.Id,
                    Status = true,
                    RegistDate = DateTime.Now,
                    Credits = sub.Credits
                };
                _db.RegistClasses.Add(rgclass);
                clas.Amount -= 1;
                _db.ClassSessions.Update(clas);
                await _db.SaveChangesAsync();
                return RedirectToAction("HocPhanDaDangKy", "Home");
            }
            else
            {
                ViewData["Description"] = " Bạn đã đăng ký lớp này rồi !!!!";
                return RedirectToAction("DanhSachLop", "Home");
            }

        }
        // ---> check học phân đã đăng ký ()
        public bool checkClass(string id)
           {
            string username = HttpContext.Session.GetString("username");
            var acc = _db.Accounts.FirstOrDefault(x => x.UserName == username);
            var clas = _db.ClassSessions.FirstOrDefault(x => x.Id == id);
            //
            var sub = _db.Subjects.FirstOrDefault(x => x.Id == clas.SubjectId);
            var stu = _db.Students.FirstOrDefault(x => x.AccountId == acc.Id);
            //
            var regclass = _db.RegistClasses.FirstOrDefault(x => x.StudentId == stu.Id && x.ClassSessionId == clas.Id);
            if (regclass != null)
            {
                return false;
            }
            else
            {
                return true; 
            }
        }
            // ham lay danh sach da dang ky cua sinh vien
         private List<RegistClass> DanhSachLopDaDangKy(string student_id)
          {
             // list sinh vien da dang ky
                List<RegistClass> lst_regist_class = new List<RegistClass>();
                lst_regist_class = _db.RegistClasses.Where(s => s.StudentId == student_id && s.Status == false).ToList();
                return lst_regist_class;      
          }     
            // hàm kiểm mon hoc da hoc chua
         public bool KiemTraLopDaDangKy(string subject_id, string student_id)
            {
                List<RegistClass> danhSachDaDangKy = DanhSachLopDaDangKy(student_id);
                List<ClassSession> danhSachLopHP = _db.ClassSessions.Where(s => s.SubjectId == subject_id).ToList();
                foreach (var item in danhSachDaDangKy)
                {
                    foreach (var item2 in danhSachLopHP)
                    {
                        if (item.ClassSessionId == item2.Id)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }     
            // kiem tra mon tien quyet
         public List<Subject> kiemTraMonTienQuyet(string subject_id)
            {
                List<Subject> subjects = new List<Subject>();

                var lst_monTienQuyet = _db.PrerequisiteSubjects.Where(s => s.SubjectId == subject_id).ToList();
                foreach (var item in lst_monTienQuyet)
                {
                    if (!String.IsNullOrEmpty(item.PrerequisiteSubjectId))
                    {
                        foreach (var item2 in _db.Subjects.Where(s => s.Id == item.PrerequisiteSubjectId))
                        {
                            subjects.Add(item2);
                        }
                    }
                }
                return subjects;
          }

        // ---> hủy đăng ký(bug)
        public IActionResult deleteLHP(string idClass)
        {
            string username = HttpContext.Session.GetString("username");
            var acc = _db.Accounts.FirstOrDefault(x => x.UserName == username);
            ClassSession clas = _db.ClassSessions.FirstOrDefault(x => x.Id == idClass);
            //
            var sub = _db.Subjects.FirstOrDefault(x => x.Id == clas.SubjectId);
            var stu = _db.Students.FirstOrDefault(x => x.AccountId == acc.Id);
            //
            RegistClass rgclass = _db.RegistClasses.FirstOrDefault(x => x.StudentId == stu.Id && x.ClassSessionId == clas.Id);
            _db.RegistClasses.Remove(rgclass);
            clas.Amount += 1;
            _db.ClassSessions.Update(clas);
            _db.SaveChanges();
            return RedirectToAction("HocPhanDaDangKy", "Home");

        }

        [HttpGet]
        public async Task<IActionResult> HuyDangKy(string id)
        {
            var danhSachDangKy = _db.RegistClasses.FirstOrDefault(x => x.ClassSessionId == id);

            if (danhSachDangKy != null)
            {
                _db.RegistClasses.Remove(danhSachDangKy);
                ClassSession updateSoLuongMonHoc = _db.ClassSessions.FirstOrDefault(x => x.Id == id);
                updateSoLuongMonHoc.Amount += 1;
                _db.ClassSessions.Update(updateSoLuongMonHoc);

                _db.SaveChangesAsync();
            }
            else
            {
                TempData["error"] = "Có lỗi gì đó~ Tui cũng không biết nữa? Khi nào ta yêu nhau";
            }
            return RedirectToAction("HocPhanDaDangKy");

        }
        /*******/
        public async Task<IActionResult> DanhSachLop(string id)
        {
            string stuID = HttpContext.Session.GetString("studentID");
            List<RegistClass> lopDaDangKy = await _db.RegistClasses.Where(x => x.StudentId == stuID).ToListAsync();
            List<ClassSession> list = await _db.ClassSessions.Where(x => x.SubjectId == id).ToListAsync();
            int checker = list.Count;
            foreach (var item in list.ToList())
            {
                foreach (var item2 in lopDaDangKy.ToList())
                {
                    if (item2.ClassSessionId == item.Id)
                    {
                        list.Remove(item);
                    }
                }

            }
            if (list.Count() < checker)
            {
                ViewData["Description"] = "Bạn đã đăng ký môn này! Hãy huỷ lớp của môn này để chọn lớp khác!";
                return View();
            }

            if (list.Count() == 0)
            {
                ViewData["Description"] = "Đã đăng ký hết !!!!! ";
                return View();
            }
            return View(list);
        }

        // ---> lớp học phần trong kế hoạch | ngoài kế hoạch
        public IActionResult ThongBao()
        {
            
            if (trongKyDangKy && RoleCheckByHand)
            {
                var list_class_nkh = from s in _db.ClassSessions where s.CommonClass == true select s;
                return View(list_class_nkh.ToList());
            }
            TempData["notification"] = "Chưa đến kỳ đăng ký!";
            return View();
        }            

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }   
    } 
}