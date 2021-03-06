using System.Text.RegularExpressions;
using GusMelfordBot.DAL.Applications.ContentCollector;

namespace GusMelfordBot.Core.Services.Apps.ContentCollector.Contents.ContentProviders.TikTok;

public static class TikTokServiceHelper
{
    private const string TIK_TOK = "tiktok";
    
    private static readonly List<string> EmojiList = new()
    {
        "š³", "š„µ", "š", "š", "ā¤ļø", "š", "š",
        "š", "š", "š", "š", "š", "š", "š", 
        "š", "š„ø", "š", "š„ŗ", "š„³", "šæ"
    };
    
    public static string GetEditedMessage(
        Content? tikTokContent,
        string accompanyingCommentary)
    {
        string text = $"{GetRandomEmoji()} Content ā {tikTokContent?.Number}\n" +
                      $"š¤ {tikTokContent?.Id}\n" +
                      $"š {tikTokContent?.User?.FirstName} {tikTokContent?.User?.LastName}\n" +
                      $"{tikTokContent?.RefererLink}";

        if (!string.IsNullOrEmpty(accompanyingCommentary))
        {
            text += $"\nš¤ {accompanyingCommentary}";
        }

        return text;
    }

    private static string GetRandomEmoji()
    {
        return EmojiList[new Random().Next(0, EmojiList.Count)];
    }
    
    public static string GetUserName(string referer)
    {
        return Regex
            .Match(referer, "com/(.*?)/video")
            .Groups[1]
            .Value;
    }

    public static string GetVideoId(string referer)
    {
        return referer
            .Replace(TIK_TOK, "")
            .Replace("/video/", " ")
            .Split(" ")[1];
    }
}