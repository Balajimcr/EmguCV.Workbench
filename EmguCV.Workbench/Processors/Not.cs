﻿using Emgu.CV;
using Emgu.CV.Structure;

namespace EmguCV.Workbench.Processors
{
    public class Not : ImageProcessor
    {
        public override void Process(ref Image<Bgr, byte> image)
        {
            image = image.Not();
        }
    }
}
