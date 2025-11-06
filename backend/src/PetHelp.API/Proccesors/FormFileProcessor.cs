using PetHelp.Application.Dto;

namespace PetHelp.API.Proccesors;

//класс использующий интерфейс IAsyncDisposable для того чтобы освобождать поток
public class FormFileProcessor : IAsyncDisposable
{
    private readonly List<UploadFileDto> _fileDtos = [];

    public List<UploadFileDto> Process(IFormFileCollection files)
    {
        foreach (var file in files)
        {
            var stream = file.OpenReadStream();
            var fileDto = new UploadFileDto(stream, file.FileName, file.ContentType);
            _fileDtos.Add(fileDto);
        }

        return _fileDtos;
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var file in _fileDtos)
        {
            await file.Content.DisposeAsync();
        }
    }
}