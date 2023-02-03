using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Crypto
{
    public static string ROTEncryption(string text, int rotOffset, Alphabet alphabet = Alphabet.LatinUppercase)
    {        
        string glyphset = ((Glyphset)((int)alphabet)).ToString();
        string retGlyphs = "";

        foreach (char c in text)
        {
            if (!glyphset.Contains(c))
            {
                retGlyphs += c;
            }
            else
            {
                int index = glyphset.IndexOf(c) + rotOffset;
                while (index < 0)
                {
                    index += glyphset.Length;
                }

                index = index % glyphset.Length;
                retGlyphs += glyphset[index];
            }
        }

        return rotOffset + "|" + (int)alphabet + "|" + retGlyphs;
    }

    public static string ROTDecryption(string text)
    {
        string retGlyphs = "";

        string[] splitText = text.Split('|');
        int offset = 0;

        int glyphsetIndex = 0;
        int.TryParse(splitText[1], out glyphsetIndex);
        string glyphset = ((Glyphset)glyphsetIndex).ToString();

        if (int.TryParse(splitText[0], out offset))
        {
            retGlyphs = ROTDecryption(text, offset);
        }

        return retGlyphs;
    }

    public static string ROTDecryption(string text, int rotValue)
    {
        string retGlyphs = "";

        string[] splitText = text.Split('|');

        int glyphsetIndex = 0;
        int.TryParse(splitText[1], out glyphsetIndex);
        string glyphset = ((Glyphset)glyphsetIndex).ToString();

        foreach (char c in splitText[2])
        {
            if (!glyphset.Contains(c))
            {
                retGlyphs += c;
            }
            else
            {
                int index = glyphset.IndexOf(c) - rotValue;
                while (index < 0)
                {
                    index += glyphset.Length;
                }

                index = index % glyphset.Length;

                retGlyphs += glyphset[index];
            }
        }

        return retGlyphs;
    }

    public static void RotTest (string input, int rotOffset, Alphabet alphabet = Alphabet.LatinUppercase)
    {
        input = Crypto.ROTEncryption(input, rotOffset, alphabet);
        Debug.Log(input);
        input = Crypto.ROTDecryption(input);
        Debug.Log(input);
    }

}

public enum Alphabet
{
    LatinUppercase = 0,
    LatinLowercase = 1,
}

public enum Glyphset
{
    ABCDEFGHIJKLMNOPQRSTUVWXYZ = 0,
    abcdefghijklmnopqrstuvwxyz = 1,

}
