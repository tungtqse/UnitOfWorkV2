using AutoMapper.QueryableExtensions;
using DomainService.Interface;
using DomainService.Model;
using GenericRepositoryandUoW.Common;
using GenericRepositoryandUoW.Model;
using GenericRepositoryandUoW.UnitOfWork;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.Services
{
    public class StudentService :IStudentService
    {
        private readonly IUnitOfWorkFactory<UnitOfWork> _unitOfWork;

        public StudentService(IUnitOfWorkFactory<UnitOfWork> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<StudentModel> Search(SearchStudentModel model)
        {
            using (var unit = _unitOfWork.Create())
            {
                var query = unit.Repository<Student>().Where(InitilizeFilter(model));               

                var result = query.Select(s => new StudentModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    Age = s.Age,
                    DateOfBirth = s.DateOfBirth
                }).OrderBy(o=>o.Name).Skip(model.Skip).Take(model.Take).ToList();

                return result;
            }
        }

        public void Add(StudentModel model)
        {
            using (var unit = _unitOfWork.Create())
            {
                var student = new Student();
                student.Id = Guid.NewGuid();
                student.Name = model.Name;
                student.Age = model.Age;
                student.DateOfBirth = model.DateOfBirth;
                student.Email = model.Email;

                unit.Repository<Student>().Add(student);

                unit.SaveChanges();
            }
        }

        public void Edit(StudentModel model)
        {
            using (var unit = _unitOfWork.Create())
            {
                var student = unit.Repository<Student>().FirstOrDefault(f => f.Id == model.Id);

                if(student != null)
                {                  
                    student.Name = model.Name;
                    student.Age = model.Age;
                    student.DateOfBirth = model.DateOfBirth;
                    student.Email = model.Email;

                    unit.SaveChanges();
                }                
            }
        }

        public StudentModel GetDetail(Guid Id)
        {
            using (var unit = _unitOfWork.Create())
            {
                var student = unit.Repository<Student>().FirstOrDefault(f => f.Id == Id);

                if(student != null)
                {
                    var model = new StudentModel();
                    model.Id = student.Id;
                    model.Name = student.Name;
                    model.Age = student.Age;
                    model.DateOfBirth = student.DateOfBirth;
                    model.Email = student.Email;

                    return model;
                }
            }

            return null;
        }

        public void Delete(Guid Id)
        {
            using (var unit = _unitOfWork.Create())
            {
                var student = unit.Repository<Student>().FirstOrDefault(f => f.Id == Id);

                if (student != null)
                {
                    student.StatusId = Constant.StatusId.InActive;

                    unit.SaveChanges();
                }
            }
        }

        private Expression<Func<Student, bool>> InitilizeFilter(SearchStudentModel model)
        {
            var filter = PredicateBuilder.New<Student>().Start(s => true).And(f=>f.StatusId == Constant.StatusId.Active);

            if (!string.IsNullOrEmpty(model.Name))
            {
                filter = filter.And(f => f.Name == model.Name);
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                filter = filter.And(f => f.Email == model.Email);
            }

            if (model.Age.HasValue)
            {
                filter = filter.And(f => f.Age == model.Age);
            }

            if (model.DateOfBirth.HasValue)
            {
                filter = filter.And(f => f.DateOfBirth == model.DateOfBirth);
            }

            return filter;
        }
    }
}
