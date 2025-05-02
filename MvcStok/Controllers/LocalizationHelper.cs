using System.Collections.Generic;

public static class LocalizationHelper
{
    // Türkçe metinler
    private static readonly Dictionary<string, string> _localizedStringsTR = new Dictionary<string, string>
    {
        { "Login", "Giriş Yap" },
        { "Username", "Kullanıcı Adı" },
        { "Password", "Şifre" },
        { "LoginButton", "Giriş" }
    };

    // İngilizce metinler
    private static readonly Dictionary<string, string> _localizedStringsEN = new Dictionary<string, string>
    {
        { "Login", "Login" },
        { "Username", "Username" },
        { "Password", "Password" },
        { "LoginButton", "Login" }
    };

    // Dil seçeneğine göre metni döndürme
    public static string GetLocalizedString(string key, string language)
    {
        // Türkçe metinler
        if (language == "tr" && _localizedStringsTR.ContainsKey(key))
            return _localizedStringsTR[key];

        // İngilizce metinler
        else if (language == "en" && _localizedStringsEN.ContainsKey(key))
            return _localizedStringsEN[key];

        // Eğer dil metni bulunamazsa, anahtar kelimeyi döndür
        return key;
    }
}
