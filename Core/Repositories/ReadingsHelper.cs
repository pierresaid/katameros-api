using Katameros.DTOs;
using Katameros.Enums;

namespace Katameros.Repositories;

public class ReadingsHelper(DatabaseContext _context)
{

    private async Task<Passage> MakePassage(string passageRef)
    {
        Passage passage = new Passage();

        var splittedPassageRef = passageRef.Split('.', ':');
        passage.BookId = int.Parse(splittedPassageRef[0]);
        passage.Chapter = int.Parse(splittedPassageRef[1]);
        List<int> splittedVersesComma = null;

        var query = _context.Verses.Where(v => v.BibleId == _context.BibleId && v.BookId == passage.BookId && v.Chapter == passage.Chapter);
        string versesRef = splittedPassageRef[2];
        if (versesRef.Contains('-'))
        {
            var splittedVerses = versesRef.Split('-');
            int from = int.Parse(string.Concat(splittedVerses[0]));
            int to = splittedVerses[1] == "end" ? -1 : int.Parse(string.Concat(splittedVerses[1]));
            if (to == -1) // To the end
                query = query.Where(v => v.Number >= from);
            else
                query = query.Where(v => v.Number >= from && v.Number <= to);
        }
        else if (versesRef.Contains(','))
        {
            splittedVersesComma = versesRef.Split(',').Select(s => int.Parse(s)).ToList();
            query = query.Where(v => splittedVersesComma.Contains(v.Number));
        }
        else
        {
            query = query.Where(v => v.Number == int.Parse(string.Concat(versesRef)));
        }
        var bookTranslation = (await _context.BooksTranslations.FindAsync(passage.BookId, _context.LanguageId))?.Text;

        passage.BookTranslation = bookTranslation;
        passage.Verses = query.ToList();
        if (versesRef.Contains(','))
        {
            passage.Verses = passage.Verses.OrderBy(v => splittedVersesComma.FindIndex(s => s == v.Number)).ToList();
        }
        if (versesRef.Contains("end") && passage.Verses.Any())
            versesRef = versesRef.Replace("end", passage.Verses.Last().Number.ToString());
        passage.Ref = $"{passage.Chapter}:{versesRef}";
        return passage;
    }

    public async Task<Reading> MakeReading(string passagesRef, ReadingType readingType, int readingMode = ReadingMode.Complete)
    {
        Reading reading = new Reading(readingType);
        List<Passage> passages = new List<Passage>();
        string[] passageRefs = GetRefs(passagesRef);

        foreach (var passageRef in passageRefs)
        {
            passages.Add(await MakePassage(passageRef));
        }

        reading.Passages = passages;
        if (readingMode == ReadingMode.Complete)
        {
            reading.Introduction = (await _context.ReadingsMetadatasTranslations.FindAsync((int)readingType, (int)ReadingsMetadata.Introduction, _context.LanguageId))?.Text;
            reading.Conclusion = (await _context.ReadingsMetadatasTranslations.FindAsync((int)readingType, (int)ReadingsMetadata.Conclusion, _context.LanguageId))?.Text;
        }

        return reading;
    }

    public string[] GetRefs(string refsStr)
    {
        var refs = refsStr.Split(new string[] { "*@+", "@" }, StringSplitOptions.None).ToList();
        var res = new List<string>();

        foreach (var refe in refs)
        {
            if (refe.IndexOf(":") != refe.LastIndexOf(":"))
            {
                string[] p = refe.Split(new string[] { "-", ":" }, StringSplitOptions.None);

                var book = p[0].Split('.')[0];
                var chapterBegin1 = p[0].Split('.')[1];
                var verseBegin1 = p[1];
                var chapterBegin2 = p[2];
                var verseBegin2 = "1";
                var verseEnd2 = p[3];
                if (p.Length > 4)
                {
                    verseBegin2 = p[3];
                    verseEnd2 = p[4];
                }

                res.Add($"{book}.{chapterBegin1}:{verseBegin1}-end");

                int chap1 = int.Parse(chapterBegin1);
                int chap2 = int.Parse(chapterBegin2);
                while (chap2 > chap1 + 1)
                {
                    chap1 += 1;
                    res.Add($"{book}.{chap1}:1-end");
                }
                res.Add($"{book}.{chapterBegin2}:{verseBegin2}-{verseEnd2}");
            }
            else
                res.Add(refe);
        }

        return res.ToArray();
    }

    public async Task<string> GetSectionMeta(SectionType sectionType, SectionsMetadata sectionsMetadata)
    {
        return (await _context.SectionsMetadatasTranslations.FindAsync((int)sectionType, (int)sectionsMetadata, _context.LanguageId))?.Text;
    }

    public async Task<string> GetSubSectionMeta(SubSectionType subSectionType, SubSectionsMetadata subSectionsMetadata)
    {
        return (await _context.SubSectionsMetadatasTranslations.FindAsync((int)subSectionType, (int)subSectionsMetadata, _context.LanguageId))?.Text;
    }

    public async Task<string> GetReadingMeta(ReadingType readingType, ReadingsMetadata readingsMetadata)
    {
        return (await _context.ReadingsMetadatasTranslations.FindAsync((int)readingType, (int)readingsMetadata, _context.LanguageId))?.Text;
    }

    public async Task<string> GetSentence(Sentence sentenceType)
    {
        return (await _context.SentencesTranslations.FindAsync((int)sentenceType, _context.LanguageId))?.Text;
    }

    public async Task<string> GetFeastTranslation(Feast feastId)
    {
        return (await _context.FeastsTranslations.FindAsync((int)feastId, _context.LanguageId))?.Text;
    }
}


//private async Task<Passage> MakePassage(string passageRef)
//{
//    Passage passage = new Passage();

//    var splittedPassageRef = passageRef.Split('.', ':');
//    passage.BookId = int.Parse(splittedPassageRef[0]);
//    passage.Chapter = int.Parse(splittedPassageRef[1]);
//    List<int> splittedVersesComma = null;
//    List<int> chapterArray = null;


//    var query = _context.Verses.Where(v => v.BibleId == this._context.BibleId && v.BookId == passage.BookId);
//    string versesRef = splittedPassageRef[2];
//    if (versesRef.Contains('-') && splittedPassageRef.Length < 4)
//    {
//        var splittedVerses = versesRef.Split('-');
//        int from = int.Parse(string.Concat(splittedVerses[0]));
//        int to = int.Parse(string.Concat(splittedVerses[1]));
//        query = query.Where(v => v.Number >= from && v.Number <= to && v.Chapter == passage.Chapter);
//    }
//    else if (versesRef.Contains('-') && splittedPassageRef.Length == 4)
//    {
//        var splittedMulti = versesRef.Split('-');

//        int secondChapter = int.Parse(string.Concat(splittedMulti[1]));
//        int from = int.Parse(string.Concat(splittedMulti[0]));

//        int to = int.Parse(string.Concat(splittedPassageRef[3]));

//        chapterArray = new List<int>() { passage.Chapter, secondChapter };

//        query = query.Where(v => (v.Number >= from && v.Chapter == passage.Chapter) || (v.Number <= to && v.Chapter == secondChapter));
//    }
//    else if (versesRef.Contains(','))
//    {
//        splittedVersesComma = versesRef.Split(',').Select(s => int.Parse(s)).ToList();
//        query = query.Where(v => splittedVersesComma.Contains(v.Number) && v.Chapter == passage.Chapter);
//    }
//    else
//    {
//        query = query.Where(v => v.Number == int.Parse(string.Concat(versesRef)) && v.Chapter == passage.Chapter);
//    }
//    var bookTranslation = (await _context.BooksTranslations.FindAsync(passage.BookId, _context.LanguageId))?.Text;
//    passage.Ref = $"{passage.Chapter}:{versesRef}";
//    passage.BookTranslation = bookTranslation;
//    passage.Verses = query.ToList();
//    if (versesRef.Contains(','))
//    {
//        passage.Verses = passage.Verses.OrderBy(v => splittedVersesComma.FindIndex(s => s == v.Number)).ToList();
//    }
//    if (versesRef.Contains('-') && splittedPassageRef.Length == 4)
//    {
//        passage.Verses.OrderBy(v => chapterArray.FindIndex(c => c == v.Chapter))
//                     .ThenBy(v => v.Number);
//    }
//    return passage;
//}