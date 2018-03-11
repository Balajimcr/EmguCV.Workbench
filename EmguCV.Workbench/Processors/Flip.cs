﻿using System.ComponentModel;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace EmguCV.Workbench.Processors
{
    public class Flip : ImageProcessor
    {
        public Flip()
        {
            FlipType = FlipType.None;
        }

        private FlipType _flipType;
        [Category("Flip")]
        [PropertyOrder(0)]
        [DisplayName(@"Flip Type")]
        [Description(@"The type of the flipping.")]
        [DefaultValue(FlipType.None)]
        public FlipType FlipType
        {
            get { return _flipType; }
            set { Set(ref _flipType, value); }
        }

        public override void Process(ref Image<Gray, byte> image)
        {
            image = image.Flip(_flipType);
        }
    }
}
