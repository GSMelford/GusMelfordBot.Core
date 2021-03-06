using GusMelfordBot.Core.Domain.Apps.ContentCollector.ContentDownload;
using GusMelfordBot.DAL.Applications.ContentCollector;
using GusMelfordBot.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GusMelfordBot.Core.Services.Apps.ContentCollector.ContentDownload;

public class ContentDownloadRepository : IContentDownloadRepository
{
    private readonly IDatabaseManager _databaseManager;
    
    public ContentDownloadRepository(IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }

    public async Task<Content?> GetContent(Guid contentId)
    {
        return await _databaseManager.Context
            .Set<Content>()
            .FirstOrDefaultAsync(x => x.Id == contentId);
    }

    public async Task SetIsNotValid(Guid contentId)
    {
        var content = await _databaseManager.Context
            .Set<Content>()
            .FirstOrDefaultAsync(x => x.Id == contentId);
        
        if (content is null)
        {
            return;
        }
        
        content.IsValid = false;
        _databaseManager.Context.Update(content);
        await _databaseManager.Context.SaveChangesAsync();
    }
}