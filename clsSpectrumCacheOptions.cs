﻿using System;

namespace MASIC
{
    public class clsSpectrumCacheOptions
    {

        /* TODO ERROR: Skipped RegionDirectiveTrivia */
        /// <summary>
    /// If True, then spectra will never be cached to disk and the spectra pool will consequently be increased as needed
    /// </summary>
        public bool DiskCachingAlwaysDisabled { get; set; }

        /// <summary>
    /// Path to the cache directory (can be relative or absolute, aka rooted); if empty, then the user's AppData directory is used
    /// </summary>
        public string DirectoryPath { get; set; }

        public int SpectraToRetainInMemory
        {
            get
            {
                return mSpectraToRetainInMemory;
            }

            set
            {
                if (value < 100)
                    value = 100;
                mSpectraToRetainInMemory = value;
            }
        }

        [Obsolete("Legacy parameter; no longer used")]
        public float MinimumFreeMemoryMB { get; set; }
        [Obsolete("Legacy parameter; no longer used")]
        public float MaximumMemoryUsageMB { get; set; }

        /* TODO ERROR: Skipped EndRegionDirectiveTrivia */
        /* TODO ERROR: Skipped RegionDirectiveTrivia */
        private int mSpectraToRetainInMemory = 1000;
        /* TODO ERROR: Skipped EndRegionDirectiveTrivia */
        public void Reset()
        {
            var defaultOptions = clsSpectraCache.GetDefaultCacheOptions();
            DiskCachingAlwaysDisabled = defaultOptions.DiskCachingAlwaysDisabled;
            DirectoryPath = defaultOptions.DirectoryPath;
            SpectraToRetainInMemory = defaultOptions.SpectraToRetainInMemory;
        }

        public override string ToString()
        {
            return "Cache up to " + SpectraToRetainInMemory + " in directory " + DirectoryPath;
        }
    }
}