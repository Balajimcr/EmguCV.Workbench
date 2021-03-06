﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using Emgu.CV;
using Emgu.CV.Structure;
using EmguCV.Workbench.Model;
using EmguCV.Workbench.Util;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace EmguCV.Workbench.Algorithms
{
    /// <summary>
    /// Finds circles in a grayscale image using the Hough transform.
    /// https://docs.opencv.org/master/d4/d70/tutorial_hough_circle.html
    /// </summary>
    /// <seealso cref="EmguCV.Workbench.Algorithms.ImageAlgorithm" />
    [Export(typeof(IImageAlgorithm))]
    public class HoughCircles : ImageAlgorithm
    {
        public override void Process(Image<Bgr, byte> image, out Image<Bgr, byte> annotatedImage, out List<object> data)
        {
            base.Process(image, out annotatedImage, out data);

            // convert image to gray scale
            // and get the circles
            var circles = image
                .Convert<Gray, byte>()
                .HoughCircles(
                    new Gray(_cannyThreshold),
                    new Gray(_accumulatorThreshold),
                    _dp,
                    _minDist,
                    _minRadius,
                    _maxRadius);

            data = new List<object>();

            // draw each circle and add to data collection
            foreach (var circle in circles[0])
            {
                annotatedImage.Draw(circle, new Bgr(_annoColor.Color()), _lineThick);
                data.Add(new Circle(circle));
            }
        }

        private byte _cannyThreshold = 100;
        [Category("Hough Circles")]
        [PropertyOrder(0)]
        [DisplayName(@"Canny Threshold")]
        [Description(@"The higher threshold of the two passed to Canny edge detector.")]
        public byte CannyThreshold
        {
            get { return _cannyThreshold; }
            set { Set(ref _cannyThreshold, value); }
        }

        private byte _accumulatorThreshold = 100;
        [Category("Hough Circles")]
        [PropertyOrder(1)]
        [DisplayName(@"Accumulator Threshold")]
        [Description(@"Accumulator threshold at the center detection stage.")]
        public byte AccumulatorThreshold
        {
            get { return _accumulatorThreshold; }
            set { Set(ref _accumulatorThreshold, value); }
        }

        private double _dp = 1;
        [Category("Hough Circles")]
        [PropertyOrder(2)]
        [DisplayName(@"dp")]
        [Description(@"Resolution of the accumulator used to detect centers of the circles.")]
        public double Dp
        {
            get { return _dp; }
            set { Set(ref _dp, value); }
        }

        private double _minDist = 30;
        [Category("Hough Circles")]
        [PropertyOrder(3)]
        [DisplayName(@"Min Distance")]
        [Description(@"Minimum distance between centers of the detected circles.")]
        public double MinDist
        {
            get { return _minDist; }
            set { Set(ref _minDist, value); }
        }

        private int _minRadius;
        [Category("Hough Circles")]
        [PropertyOrder(4)]
        [DisplayName(@"Min Radius")]
        [Description(@"Minimal radius of the circles to search for.")]
        public int MinRadius
        {
            get { return _minRadius; }
            set { Set(ref _minRadius, value); }
        }

        private int _maxRadius;
        [Category("Hough Circles")]
        [PropertyOrder(5)]
        [DisplayName(@"Max Radius")]
        [Description(@"Maximal radius of the circles to search for.")]
        public int MaxRadius
        {
            get { return _maxRadius; }
            set { Set(ref _maxRadius, value); }
        }
    }
}
