using School.Data;

namespace School.API.Controllers
{
    public class ControllerBase : Microsoft.AspNetCore.Mvc.Controller
    {
        private ISchoolData _repository = null;

        internal ISchoolData Repository
        {
            get
            {
                if (_repository == null)
                {
                    _repository = new SchoolServiceRepository(new SchoolContext());
                }

                return _repository;
            }
            set
            {
                _repository = value;
            }
        }
    }
}