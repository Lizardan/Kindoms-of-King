using System;
using System.Net;

public static class IpJoinCode
{
    // Алфавит Base62: цифры → заглавные буквы → строчные буквы
    private const string Base62Chars =
        "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    /// <summary>
    /// Преобразует IPv4-адрес (например "93.92.200.109") в короткий код.
    /// </summary>
    public static string IpToJoinCode(string ip)
    {
        byte[] bytes = IPAddress.Parse(ip).GetAddressBytes();
        if (bytes.Length != 4)
            throw new ArgumentException("Поддерживаются только IPv4-адреса");

        // Преобразуем 4 байта в 32-битное беззнаковое целое
        uint ipInt = ((uint)bytes[0] << 24) |
                     ((uint)bytes[1] << 16) |
                     ((uint)bytes[2] << 8) |
                      bytes[3];

        return EncodeBase62(ipInt);
    }

    /// <summary>
    /// Преобразует короткий код обратно в IPv4-адрес.
    /// </summary>
    public static string JoinCodeToIp(string code)
    {
        uint ipInt = 0;
        foreach (char c in code)
        {
            int digit = Base62Chars.IndexOf(c);
            if (digit == -1)
                throw new ArgumentException($"Недопустимый символ '{c}' в коде");
            ipInt = ipInt * 62 + (uint)digit;
        }

        byte[] bytes = new byte[4];
        bytes[0] = (byte)(ipInt >> 24);
        bytes[1] = (byte)(ipInt >> 16);
        bytes[2] = (byte)(ipInt >> 8);
        bytes[3] = (byte)ipInt;

        return new IPAddress(bytes).ToString();
    }

    // Внутренний метод для кодирования числа в Base62
    private static string EncodeBase62(uint value)
    {
        if (value == 0)
            return "0";

        // Для 32-битного числа максимальная длина в Base62 – 6 символов
        char[] buffer = new char[6];
        int index = buffer.Length - 1;

        while (value > 0)
        {
            buffer[index--] = Base62Chars[(int)(value % 62)];
            value /= 62;
        }

        return new string(buffer, index + 1, buffer.Length - index - 1);
    }
}