﻿using System.ComponentModel;
using Emgu.CV;
using Emgu.CV.Structure;
using EmguCV.Workbench.Util;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace EmguCV.Workbench.Processors
{
    public class Dilate : ImageProcessor
    {
        public Dilate()
        {
            Iterations = 0;
        }

        private int _iterations;
        [Category("Dilate")]
        [PropertyOrder(0)]
        [DisplayName(@"Iterations")]
        [Description(@"The number of dilate iterations.")]
        [DefaultValue(0)]
        public int Iterations
        {
            get { return _iterations; }
            set { Set(ref _iterations, value.Clamp(0, int.MaxValue)); }
        }

        public override void Process(ref Image<Gray, byte> image)
        {
            image = image.Dilate(_iterations);
        }
    }
}
