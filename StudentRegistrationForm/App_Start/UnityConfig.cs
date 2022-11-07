using Repository.Repository;
using ServiceLayer.ServiceLayer;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace StudentRegistrationForm
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IStudentRepository, StudentRepository>();
            container.RegisterType<IGradeRepository, GradeRepository>();
            container.RegisterType<ISubjectRepository, SubjectRepository>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IGradeService, GradeService>();
            container.RegisterType<ISubjectService, SubjectService>();
            container.RegisterType<IStudentService, StudentService>();
            container.RegisterType<ISubjectService, SubjectService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}