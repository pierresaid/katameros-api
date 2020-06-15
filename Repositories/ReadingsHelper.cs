using Katameros.DTOs;
using Katameros.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public class ReadingsHelper
    {
        private readonly DatabaseContext _context;
        public ReadingsHelper(DatabaseContext context) => _context = context;

        private async Task<Passage> MakePassage(string passageRef)
        {
            Passage passage = new Passage();

            var splittedPassageRef = passageRef.Split('.', ':');
            passage.BookId = int.Parse(splittedPassageRef[0]);
            passage.Chapter = int.Parse(splittedPassageRef[1]);
            List<int> splittedVersesComma = null;

            var query = _context.Verses.Where(v => v.BibleId == this._context.BibleId && v.BookId == passage.BookId && v.Chapter == passage.Chapter);
            string versesRef = splittedPassageRef[2];
            if (versesRef.Contains('-'))
            {
                var splittedVerses = versesRef.Split('-');
                int from = int.Parse(string.Concat(splittedVerses[0]));
                int to = int.Parse(string.Concat(splittedVerses[1]));
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
            var bookTranslation = (await _context.BooksTranslations.FindAsync(passage.BookId, _context.LanguageId)).Text;
            passage.Ref = $"{passage.Chapter}:{versesRef}";
            passage.BookTranslation = bookTranslation;
            passage.Verses = query.ToList();
            if (versesRef.Contains(','))
            {
                passage.Verses = passage.Verses.OrderBy(v => splittedVersesComma.FindIndex(s => s == v.Number)).ToList();
            }
            return passage;
        }

        public async Task<Reading> MakeReading(string passagesRef, ReadingType readingType)
        {
            Reading reading = new Reading();
            List<Passage> passages = new List<Passage>();
            string[] passageRefs = GetRefs(passagesRef);

            foreach (var passageRef in passageRefs)
            {
                passages.Add(await MakePassage(passageRef));
            }

            reading.Passages = passages;
            reading.Introduction = (await _context.ReadingsMetadatasTranslations.FindAsync((int)readingType, (int)ReadingsMetadata.Introduction, _context.LanguageId))?.Text;
            reading.Conclusion = (await _context.ReadingsMetadatasTranslations.FindAsync((int)readingType, (int)ReadingsMetadata.Conclusion, _context.LanguageId))?.Text;

            return reading;
        }

        public string[] GetRefs(string refs)
        {
            return refs.Split(new string[] { "*@+", "@" }, StringSplitOptions.None);
        }
    }
}
