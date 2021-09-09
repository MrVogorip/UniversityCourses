using System;
using System.Collections.Generic;
namespace PointAffiliation
{
    public class PolygonException : Exception
    {
        public Object Sender { get; }
        public PolygonException(Object sender, string message) : base(message)
        {
            Sender = sender;
        }
    }
    public class Polygon
    {
        bool isEnoughDots = false;
        bool isPolygonBuilt = false;
        readonly List<Dot> dots = new List<Dot>(16);
        readonly List<Segment> borders = new List<Segment>(16);
        public List<Dot> Dots { get { return dots; } }
        public List<Segment> Borders { get { return borders; } }
        public bool IsEnoughDots { get { return isEnoughDots; } }
        public bool IsPolygonBuilt { get { return isPolygonBuilt; } }
        public bool? DotAdd(Dot dot)
        {
            int dotsCount = dots.Count;
            if (dotsCount > 0)
            {
                if (PrimitivesMath.DotsEqual(dot, dots[0])) throw new PolygonException(this, "Error");
                Segment segNew = new Segment(dots[dotsCount - 1], dot);
                Segment seg = new Segment();
                for (int i = 0; i < dotsCount - 1; i++)
                {
                    seg.Set(dots[i], dots[i + 1]);
                    if (PrimitivesMath.DotOnSegment(dot, seg))
                        throw new PolygonException(this, "Error");
                    if (PrimitivesMath.SegmentsCrossing(seg, segNew))
                        throw new PolygonException(this, "Error");
                }
            }
            dots.Add(dot);
            if (dots.Count >= 3) isEnoughDots = true;
            return true;
        }
        public void Clear()
        {
            dots.Clear();
            borders.Clear();
            isEnoughDots = false;
            isPolygonBuilt = false;
        }
        public bool? Build()
        {
            if (!IsEnoughDots) return null;
            int i, dotsCount = dots.Count; 
            Segment segEnd = new Segment(dots[dotsCount - 1], dots[0]);
            Segment seg = new Segment();
            for (i = 0; i < dotsCount - 1; i++)
            {
                seg.Set(dots[i], dots[i + 1]);
                if (PrimitivesMath.SegmentsCrossing(seg, segEnd))
                    throw new PolygonException(this, "Error");
            } 
            borders.Clear();
            for (i = 0; i < dotsCount; ++i) { borders.Add(new Segment(dots[i], dots[(i + 1) % dotsCount])); }
            return isPolygonBuilt = true;
        }
        public bool TestDotOnPolygon(Dot dot0)
        {
            if ((!IsEnoughDots) || (borders.Count < 3)) return false;
            foreach (Segment seg in borders) if (PrimitivesMath.DotOnSegment(dot0, seg)) return true;
            int i, dotsCount = dots.Count;
            double CrossProd, DotProd,
                ratioDotToPolygon = 0;
            List<VectorDot1> vectorsTest = new List<VectorDot1>(dotsCount);
            for (i = 0; i < dotsCount; ++i) { vectorsTest.Add(PrimitivesMath.MakeVectorDot1(dot0, dots[i])); }
            for (i = 0; i < dotsCount; ++i)
            {
                CrossProd = PrimitivesMath.CrossProduct(vectorsTest[i], vectorsTest[(i + 1) % dotsCount]);
                DotProd = PrimitivesMath.DotProduct(vectorsTest[i], vectorsTest[(i + 1) % dotsCount]);
                ratioDotToPolygon += Math.Atan2(CrossProd, DotProd);
            }
            if ((int)ratioDotToPolygon == 0) return false;
            return true;
        }
    }
}
