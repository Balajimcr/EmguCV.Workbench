﻿using System.ComponentModel;
using System.ComponentModel.Composition;
using Emgu.CV;
using Emgu.CV.Structure;
using EmguCV.Workbench.Util;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace EmguCV.Workbench.Processors
{
    /// <summary>
    /// Calculates the image derivative by convolving the image with the appropriate kernel.
    /// The Sobel operators combine Gaussian smoothing and differentiation so the result is more 
    /// or less robust to the noise. Most often the function is called with 
    /// (xorder=1, yorder=0, aperture_size=3) or (xorder=0, yorder=1, aperture_size=3) 
    /// to calculate first x- or y- image derivative.
    /// </summary>
    /// <seealso cref="EmguCV.Workbench.Processors.ImageProcessor" />
    [Export(typeof(IImageProcessor))]
    public class Sobel : ImageProcessor
    {
        private SobelOrder _order;
        [Category("Sobel")]
        [PropertyOrder(0)]
        [DisplayName(@"Order")]
        [Description(@"Order of the derivative.")]
        public SobelOrder Order
        {
            get { return _order; }
            set { Set(ref _order, value); }
        }

        private int _orderValue = 1;
        [Category("Sobel")]
        [PropertyOrder(1)]
        [DisplayName(@"Order Value")]
        public int YOrder
        {
            get { return _orderValue; }
            set { Set(ref _orderValue, value.Clamp(1, _apertureSize - 1)); }
        }

        private int _apertureSize = 7;
        [Category("Sobel")]
        [PropertyOrder(2)]
        [DisplayName(@"Aperture Size")]
        [Description(@"Size of the extended Sobel kernel.")]
        public int ApertureSize
        {
            get { return _apertureSize; }
            set { Set(ref _apertureSize, value.ClampOdd(_apertureSize, 1, 31)); }
        }

        public override void Process(ref Image<Bgr, byte> image)
        {
            image = image
                .Convert<Bgr, float>()
                .Sobel(
                    _order == SobelOrder.X ? _orderValue : 0,
                    _order == SobelOrder.Y ? _orderValue : 0,
                    _apertureSize)
                .Convert<Bgr, byte>();
        }
    }
}
