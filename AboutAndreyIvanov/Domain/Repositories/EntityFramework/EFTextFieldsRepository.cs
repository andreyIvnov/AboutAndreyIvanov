using AboutAndreyIvanov.Domain.Entittes;
using AboutAndreyIvanov.Domain.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AboutAndreyIvanov.Domain.Repositories.EntityFramework
{
    public class EFTextFieldsRepository: ITextFieldsRepository
    {
        private readonly AppDbContext _context;
        public EFTextFieldsRepository(AppDbContext context)
        {
            _context = context;
        }



        public TextField GetTextFieldByCodeWord(string codeWord)
        {
            return _context.TextFields.FirstOrDefault(x => x.CodeWord == codeWord);
        }
        public TextField GetTextFieldById(Guid id)
        {
            return _context.TextFields.FirstOrDefault(x => x.Id == id);
        }
        public IQueryable<TextField> GetTextFields()
        {
            return _context.TextFields;
        }
        public void SaveTextField(TextField entity)
        {
            if(entity.Id == default)   // IF a created entity has default (created now) ID 
            {
                _context.Entry(entity).State = EntityState.Added;   // Mark it with flag "Added"
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified; // if a entity was created before and has little change is mark it by "Modified" flag
            }

            _context.SaveChanges();
        }
        public void DeleteTextField(Guid id)
        {
            _context.TextFields.Remove(new TextField() { Id = id });  //referance to Object we need by create a new object with some ID
            _context.SaveChanges();
        }

    }
}
