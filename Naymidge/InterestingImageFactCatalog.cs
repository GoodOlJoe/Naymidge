using MetadataExtractor;

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
            public List<FactSourceCandidate> CandidateSources = new();
        }
        /*
         * The Catalog is a list of facts we're interested in gathering for this image, plus one
         * or more sources of where that fact could come from.
         * 
         * For every interesting fact, we define a descriptor (the main name that will be used
         * for this fact's value) and an ordered list of potential sources for that fact. We will 
         * look through the sources in order and stop once we find one.
         */
        private static readonly Dictionary<string, FactSource> Catalog = new()
        {
            {
                "Date Taken",
                new FactSource
                {
                    Descriptor = "Date Taken",
                    CandidateSources = new List<FactSourceCandidate>()
                    {
                        new() {
                            FactSourceCandidateType = FactSourceCandidateType.MetaData,
                            Directory = "Exif IFD0",
                            Name = "Date/Time"
                        },
                        new() {
                            FactSourceCandidateType = FactSourceCandidateType.MetaData,
                            Directory = "Exif SubIFD",
                            Name = "Date/Time Original"
                        },
                        new() {
                            FactSourceCandidateType = FactSourceCandidateType.MetaData,
                            Directory = "GPS",
                            Name = "GPS Date Stamp"
                        },
                        new() {
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
                    CandidateSources = new List<FactSourceCandidate>()
                    {
                        new() {
                            FactSourceCandidateType = FactSourceCandidateType.MetaData,
                            Directory = "Exif SubIFD",
                            Name = "Time Zone"
                        },
                        new() {
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
                    CandidateSources = new List<FactSourceCandidate>()
                    {
                        new() {
                            FactSourceCandidateType = FactSourceCandidateType.MetaData,
                            Directory = "Exif IFD0",
                            Name = "Model"
                        },
                        new() {
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
                    CandidateSources = new List<FactSourceCandidate>()
                    {
                        new() {
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
                    CandidateSources = new List<FactSourceCandidate>()
                    {
                        new() {
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
                    CandidateSources = new List<FactSourceCandidate>()
                    {
                        new() {
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
                    CandidateSources = new List<FactSourceCandidate>()
                    {
                        new() {
                            FactSourceCandidateType = FactSourceCandidateType.MetaData,
                            Directory = "Apple Makernote",
                            Name = "Photo Identifier"
                        },
                    },
                }
            },
            {
                "Image Orientation",
                new FactSource
                {
                    Descriptor = "Image Orientation",
                    CandidateSources = new List<FactSourceCandidate>()
                    {
                        new() {
                            FactSourceCandidateType = FactSourceCandidateType.MetaData,
                            Directory = "Exif IFD0",
                            Name = "Orientation"
                        },
                    },
                }
            },
            {
                "Video Orientation",
                new FactSource
                {
                    Descriptor = "Video Orientation",
                    CandidateSources = new List<FactSourceCandidate>()
                    {
                        new() {
                            FactSourceCandidateType = FactSourceCandidateType.MetaData,
                            Directory = "QuickTime Track Header",
                            Name = "Rotation"
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
                null == Catalog[descriptor].CandidateSources ||
                0 == Catalog[descriptor].CandidateSources.Count
            )
            {
                return retval;
            }

            List<FactSourceCandidate> candidateSources = Catalog[descriptor].CandidateSources;

            // go through the Catalog's list of order sources for this descriptor, try to find a match
            foreach (FactSourceCandidate candidateSource in candidateSources)
            {
                switch (candidateSource.FactSourceCandidateType)
                {
                    case FactSourceCandidateType.MetaData:
                        // this candidate source is a metadata tag
                        foreach (MetadataExtractor.Directory dir in finst.MetadataDirectories.Where(d => d.Name.Equals(candidateSource.Directory)))
                        {
                            foreach (Tag tag in dir.Tags.Where(t => t.Name.Equals(candidateSource.Name)))
                            {
                                if (!string.IsNullOrEmpty(tag.Description))
                                {
                                    retval = tag.Description;
                                    break;
                                }
                            }
                            if (!string.IsNullOrEmpty(retval)) break;
                        }
                        break;

                    case FactSourceCandidateType.FileInfo:
                        // this candidate source is a file attribute
                        FileInfo fi = new(finst.FQN);
                        switch (candidateSource.Name)
                        {
                            // these case values have to match what is in the FactSourceCandidate specification.
                            // Basically a text mapping of property name to actual compiled property. So only
                            // the properties that we know are specified in some FactSourceCandidate need to be
                            // accounted for here.
                            case "CreationTime":
                                fi.CreationTime.ToString("yyyy MM dd HH:mm");
                                break;
                        }
                        break;
                }
                if (!string.IsNullOrEmpty(retval)) break;
            }

            return retval;
        }
    }
}
