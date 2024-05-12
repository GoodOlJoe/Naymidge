using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Naymidge
{
    internal static class InterestingImageFactCatalog
    {
        private enum FactSourceCandidateType
        {
            FileInfo,
            MetaData
        }
        private class FactSourceCandidate
        {
            public FactSourceCandidateType FactSourceCandidateType { get; set; }
            public string Directory { get; set; } = "";
            public string Name { get; set; } = "";
        }
        private class FactSource
        {
            public string Descriptor = "";
            public List<FactSourceCandidate> Candidates = new List<FactSourceCandidate>();
        }
        /*
         * The Catalog is a list of facts we're interested in gathering for this image, plus one
         * or more sources of where that fact could come from.
         * 
         * For every interesting fact, we define a descriptor (the main name that will be used
         * for this fact's value) and an ordered list of potential sources for that fact. We will 
         * look through the sources in order and stop once we find one.
         */
        private static Dictionary<string, FactSource> Catalog = new Dictionary<string, FactSource>()
        {
            {
                "Date Taken",
                new FactSource
                {
                    Descriptor = "Date Taken",
                    Candidates = new List<FactSourceCandidate>()
                    {
                        new FactSourceCandidate
                        {
                            FactSourceCandidateType = FactSourceCandidateType.MetaData,
                            Directory = "Exif IFD0",
                            Name = "Date/Time"
                        },
                        new FactSourceCandidate
                        {
                            FactSourceCandidateType = FactSourceCandidateType.MetaData,
                            Directory = "Exif SubIFD",
                            Name = "Date/Time Original"
                        },
                        new FactSourceCandidate
                        {
                            FactSourceCandidateType = FactSourceCandidateType.MetaData,
                            Directory = "GPS",
                            Name = "GPS Date Stamp"
                        },
                        new FactSourceCandidate
                        {
                            FactSourceCandidateType = FactSourceCandidateType.FileInfo,
                            Directory = "FileInfo",
                            Name = "CreationTime"
                        },
                    },
                }
            },
            {
                "Time Zone Taken",
                new FactSource
                {
                    Descriptor = "Time Zone Taken",
                    Candidates = new List<FactSourceCandidate>()
                    {
                        new FactSourceCandidate
                        {
                            FactSourceCandidateType = FactSourceCandidateType.MetaData,
                            Directory = "Exif SubIFD",
                            Name = "Time Zone"
                        },
                        new FactSourceCandidate
                        {
                            FactSourceCandidateType = FactSourceCandidateType.MetaData,
                            Directory = "Exif SubIFD",
                            Name = "Time Zone Original"
                        },
                    },
                }
            },
            {
                "Camera Description",
                new FactSource
                {
                    Descriptor = "Camera Description",
                    Candidates = new List<FactSourceCandidate>()
                    {
                        new FactSourceCandidate
                        {
                            FactSourceCandidateType = FactSourceCandidateType.MetaData,
                            Directory = "Exif IFD0",
                            Name = "Model"
                        },
                        new FactSourceCandidate
                        {
                            FactSourceCandidateType = FactSourceCandidateType.MetaData,
                            Directory = "Exif IFD0",
                            Name = "Host Computer"
                        },
                    },
                }
            },
            {
                "Latitude",
                new FactSource
                {
                    Descriptor = "Latitude",
                    Candidates = new List<FactSourceCandidate>()
                    {
                        new FactSourceCandidate
                        {
                            FactSourceCandidateType = FactSourceCandidateType.MetaData,
                            Directory = "GPS",
                            Name = "GPS Latitude"
                        },
                    },
                }
            },
            {
                "Longitude",
                new FactSource
                {
                    Descriptor = "Longitude",
                    Candidates = new List<FactSourceCandidate>()
                    {
                        new FactSourceCandidate
                        {
                            FactSourceCandidateType = FactSourceCandidateType.MetaData,
                            Directory = "GPS",
                            Name = "GPS Longitude"
                        },
                    },
                }
            },
            {
                "Image Direction",
                new FactSource
                {
                    Descriptor = "Image Direction",
                    Candidates = new List<FactSourceCandidate>()
                    {
                        new FactSourceCandidate
                        {
                            FactSourceCandidateType = FactSourceCandidateType.MetaData,
                            Directory = "GPS",
                            Name = "GPS Img Direction"
                        },
                    },
                }
            },
            {
                "Unique ID",
                new FactSource
                {
                    Descriptor = "Unique ID",
                    Candidates = new List<FactSourceCandidate>()
                    {
                        new FactSourceCandidate
                        {
                            FactSourceCandidateType = FactSourceCandidateType.MetaData,
                            Directory = "Apple Makernote",
                            Name = "Photo Identifier"
                        },
                    },
                }
            },
        };

        public static string GetValueFor(string descriptor, FileInstruction finst)
        {
            string retval = "";
            if
            (
                finst == null ||
                finst.MetadataDirectories == null ||
                string.IsNullOrEmpty(descriptor) ||
                !Catalog.ContainsKey(descriptor) ||
                null == Catalog[descriptor].Candidates ||
                0 == Catalog[descriptor].Candidates.Count
            )
            {
                return retval;
            }

            List<FactSourceCandidate> candidates = Catalog[descriptor].Candidates;

            // it's not this code, this is just a stub to remember how the directory/tag structure works.
            // go through the Catalog's list of order sources for this descriptor, try to find a match
            //foreach (var dir in finst.MetadataDirectories )
            //    foreach (var tag in dir.Tags)
            //        Debug.WriteLine($"{dir.Name} - {tag.Name} = {tag.Description}");

            return retval;
        }
    }
}
