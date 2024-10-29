
using MVCUYGPROJE.Aplication.DTOs.BookDTOs;
using MVCUYGPROJE_Domain.Entities;
using MVCUYGPROJE_Domain.Utilities.Concretes;
using MVCUYGPROJE_Domain.Utilities.Interfaces;
using MVCUYGPROJE_Infrastructure.Repositories.BookRepositories;


namespace MVCUYGPROJE.Aplication.Services.BookServices;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IDataResult<BookDTO>> CreateAsync(BookCreateDTO bookCreateDTO)
    {
        
        if (await _bookRepository.AnyAsync(x => x.Name.ToLower() == bookCreateDTO.Name.ToLower()))
        {
            return new ErrorDataResult<BookDTO>("Mevcut kitap sistemde kayıtlı!");
        }
        var newBook = bookCreateDTO.Adapt<Book>();
        await _bookRepository.AddAsync(newBook);
        await _bookRepository.SaveChangesAsync();
        
        return new SuccessDataResult<BookDTO>(newBook.Adapt<BookDTO>(), "Kitap Ekleme Başarılı!");
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var deletingBook= await _bookRepository.GetByIdAsync(id);
        if (deletingBook == null)
        {
            return new ErrorResult("Silmek istediğiniz kitap bulunamadı!");
        }
        await _bookRepository.DeleteAsync(deletingBook);
        await _bookRepository.SaveChangesAsync();
        return new SuccessResult("Kitap silme işlemi başarılı!");
    }

    public async Task<IDataResult<List<BookListDTO>>> GetAllAsync()
    {
        var books = _bookRepository.GetAllAsync();
        if (books.Result.Count() <= 0)
        {
            return new ErrorDataResult<List<BookListDTO>>(books.Result.Adapt<List<BookListDTO>>(), "Listelenecek kitap bulunamadı!");
        }
        return new SuccessDataResult<List<BookListDTO>>(books.Result.Adapt<List<BookListDTO>>(), "Kitaplar listeleme başarılı!");
    }

    public async Task<IDataResult<List<BookListDTO>>> GetAllProfileUserAsync()
    {
        var books = await _bookRepository.GetAllAsync(x=>x.IsAvailable==true);

        if (books.Count() <= 0)
        {
            return new ErrorDataResult<List<BookListDTO>>(books.Adapt<List<BookListDTO>>(), "Listelenecek kitap bulunamadı!");
        }
        return new SuccessDataResult<List<BookListDTO>>(books.Adapt<List<BookListDTO>>(), "Kitaplar listeleme başarılı!");
    }


    public async Task<IDataResult<BookDTO>> GetByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book is null)
        {
            return new ErrorDataResult<BookDTO>("Gösterilecek kitap bulunamadı!");
        }
        var bookDto = book.Adapt<BookDTO>();
        return new SuccessDataResult<BookDTO>(bookDto, "Kitap gösterme başarılı!");
    }

    public async Task<IDataResult<List<BookListDTO>>> GetByIdAsync()
    {
        var books = _bookRepository.GetAllAsync();
        if (books.Result.Count() <= 0)
        {
            return new ErrorDataResult<List<BookListDTO>>(books.Result.Adapt<List<BookListDTO>>(), "Listelenecek kitap bulunamadı!");
        }
        return new SuccessDataResult<List<BookListDTO>>(books.Result.Adapt<List<BookListDTO>>(), "Kitaplar listeleme başarılı!");
    }

    public async Task<IDataResult<BookDTO>> UpdateAsync(BookUpdateDTO bookUpdateDTO)
    {
        var updatingBook= await _bookRepository.GetByIdAsync(bookUpdateDTO.Id);
        if (updatingBook == null)
        {
            return new ErrorDataResult<BookDTO>("Güncellenecek kitap bulunamadı!");
        }
        var updatedBook = bookUpdateDTO.Adapt(updatingBook);
        await _bookRepository.UpdateAsync(updatedBook);
        await _bookRepository.SaveChangesAsync();
        return new SuccessDataResult<BookDTO>(updatedBook.Adapt<BookDTO>(), "Kitap güncelleme başarılı!");
    }

    public async Task UpdateAvailability(Guid id, bool isAvailable)
    {



        var book = await _bookRepository.GetByIdAsync(id);
        book.IsAvailable = isAvailable;
        await _bookRepository.UpdateAsync(book);
        await _bookRepository.SaveChangesAsync();

    }
}
