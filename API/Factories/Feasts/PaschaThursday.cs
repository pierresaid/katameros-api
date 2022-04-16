﻿using Katameros.DTOs;
using Katameros.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public partial class FeastsFactory
    {
        private async Task<DayReadings> ConstructPaschaThursday()
        {
            DayReadings dayReadings = new DayReadings();
            dayReadings.Title = await GetFeastTranslation(Feast.PaschaThursday);

            var FirstHour = await _readingsHelper.GetSectionMeta(SectionType.FirstHour, SectionsMetadata.Title);
            var ThirdHour = await _readingsHelper.GetSectionMeta(SectionType.ThirdHour, SectionsMetadata.Title);
            var SixthHour = await _readingsHelper.GetSectionMeta(SectionType.SixthHour, SectionsMetadata.Title);
            var NinthHour = await _readingsHelper.GetSectionMeta(SectionType.NinthHour, SectionsMetadata.Title);
            var EleventhHour = await _readingsHelper.GetSectionMeta(SectionType.EleventhHour, SectionsMetadata.Title);

            var EveOf = await _readingsHelper.GetSentence(Sentence.EveOf);
            var NextDay = await GetFeastTranslation(Feast.PaschaFriday);

            dayReadings.Sections = new List<Section>() {
                new Section(SectionType.FirstHour)
                {
                    Title = FirstHour,
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("2.17:8-16"),
                        await _readingsRepository.MakeOldTestament("2.15:22-16:3"),
                        await _readingsRepository.MakeOldTestament("23.58:1-9"),
                        await _readingsRepository.MakeOldTestament("26.18:20-32"),
                        await _readingsRepository.MakeActs("44.1:15-20"),
                        await _readingsRepository.MakePsalmAndGospel("19.54:22,13", "42.22:7-13"),
                    }
                },
                new Section(SectionType.ThirdHour)
                {
                    Title = ThirdHour,
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("2.32:30-33:5"),
                        await _readingsRepository.MakeOldTestament("73.24:1-11"),
                        await _readingsRepository.MakeOldTestament("38.9:11-14"),
                        await _readingsRepository.MakeOldTestament("22.30:2-6"),
                        await _readingsRepository.MakePsalmAndGospel("19.93:21,23", "40.26:17-19"),
                    }
                },
                new Section(SectionType.SixthHour)
                {
                    Title = SixthHour,
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("24.7:2-15"),
                        await _readingsRepository.MakeOldTestament("26.20:39-44"),
                        await _readingsRepository.MakeOldTestament("73.12:13-13:1"),
                        await _readingsRepository.MakePsalmAndGospel("19.30:19,14", "41.14:12-16"),
                    }
                },
                new Section(SectionType.NinthHour)
                {
                    Title = NinthHour,
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("1.22:1-19"),
                        await _readingsRepository.MakeOldTestament("23.61:1-7"),
                        await _readingsRepository.MakeOldTestament("1.14:17-20"),
                        await _readingsRepository.MakeOldTestament("18.27:2-28:13"),
                        await _readingsRepository.MakePsalmAndGospel("19.22:1", "40.26:17-19"),
                    }
                },
                // todo lakane + liturgie
                new Section(SectionType.EleventhHour)
                {
                    Title = EleventhHour,
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("23.52:13-53:12"),
                        await _readingsRepository.MakeOldTestament("23.19:19-25"),
                        await _readingsRepository.MakeOldTestament("38.12:11-14:1-4*@+38.14:6-9"),
                        await _readingsRepository.MakePsalmAndGospel("19.49:17,18", "43.13:21-30"),
                    }
                },
                new Section(SectionType.EveFirstHour)
                {
                    Title = $"{FirstHour} {EveOf} {NextDay}",
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("24.8:17-9:6"),
                        await _readingsRepository.MakePsalmAndGospel("19.101:1,9", "43.13:33-14:25"),
                        await _readingsRepository.MakeGospel("43.14:26-15:25"),
                        await _readingsRepository.MakeGospel("43.15:26-16:33"),
                        await _readingsRepository.MakeGospel("43.17:1-26"),

                    }
                },
                new Section(SectionType.EveThirdHour)
                {
                    Title = $"{ThirdHour} {EveOf} {NextDay}",
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("30.4:4-13"),
                        await _readingsRepository.MakePsalmAndGospel("19.55:21,1", "41.14:3-11"),
                    }
                },
                new Section(SectionType.EveSixthHour)
                {
                    Title = $"{SixthHour} {EveOf} {NextDay}",
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("30.3:1-11"),
                        await _readingsRepository.MakePsalmAndGospel("19.140:1,2", "43.12:36-43"),
                    }
                },
                new Section(SectionType.EveNinthHour)
                {
                    Title = $"{NinthHour} {EveOf} {NextDay}",
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("26.20:27-33"),
                        await _readingsRepository.MakePsalmAndGospel("19.7:1-2", "43.10:29-38"),
                    }
                },
                new Section(SectionType.EveEleventhHour)
                {
                    Title = $"{EleventhHour} {EveOf} {NextDay}",
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("24.8:4-9"),
                        await _readingsRepository.MakePsalmAndGospel("19.62:7,6", "43.12:44-50"),
                    }
                }
            };
            return dayReadings;
        }
    }
}
