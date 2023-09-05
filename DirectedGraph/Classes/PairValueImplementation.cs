using DirectedGraph.Interfaces;

namespace DirectedGraph.Classes
{
    public class PairValueImplementation<T>
    {
        public T T1 { get; set; }
        public T T2 { get; set; }
        public double Cx { get; set; }
        public double Cy { get; set; }
        public double Length { get; set; }
        public double Angle { get; set; }

        public T Start { get { return T1; } }
        public T End { get { return T2; } }

        public PairValueImplementation(T t1, T t2)
        {

            T1 = t1;
            T2 = t2;
        }
        public bool Contains(T value)
        {
            return value.Equals(T1) || value.Equals(T2);
        }

        public override bool Equals(object o) 
        {
            if(o == null || o.GetType() != typeof(PairValueImplementation<T>))
                return false;
            PairValueImplementation<T> casted = (PairValueImplementation<T>) o;
            return casted.T1.Equals(T1) && casted.T2.Equals(T2);
        
        }

        public override int GetHashCode()
        {
            return T1.GetHashCode() + T2.GetHashCode();
        }
    }
}
