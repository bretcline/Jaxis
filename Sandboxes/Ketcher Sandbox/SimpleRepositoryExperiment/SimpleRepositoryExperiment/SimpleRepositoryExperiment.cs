using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic.Repository;
using SubSonic.SqlGeneration.Schema;

namespace SimpleRepositoryExperiment
{
    class SimpleRepositoryExperiment
    {
        static void Main(string[] args)
        {
            SimpleRepository Repo = new SimpleRepository("Experimental", SimpleRepositoryOptions.RunMigrations);

            //ChildTable CT1 = new ChildTable();
            //ChildTable CT2 = new ChildTable();
            //ChildTable CT3 = new ChildTable();

            //CT1.ChildTableID = 1;
            //CT1.Value = "win";
            //CT2.ChildTableID = 2;
            //CT2.Value = "lose";
            //CT3.ChildTableID = 3;
            //CT3.Value = "tie";

            //List<ChildTable> ChildList = new List<ChildTable> { CT1, CT2, CT3 };

            ParentTable PT = new ParentTable();
            PT.ParentTableID = 1;
            PT.ParentValue = "I'm the parent.";
            //PT.Children = ChildList;

            //if (Repo.Exists<ChildTable>(C => C.ChildTableID == CT1.ChildTableID))
            //{
            //    Repo.Update(CT1);
            //}
            //else
            //{
            //    Repo.Add(CT1);
            //}

            //if (Repo.Exists<ChildTable>(C => C.ChildTableID == CT2.ChildTableID))
            //{
            //    Repo.Update(CT2);
            //}
            //else
            //{
            //    Repo.Add(CT2);
            //}

            //if (Repo.Exists<ChildTable>(C => C.ChildTableID == CT3.ChildTableID))
            //{
            //    Repo.Update(CT3);
            //}
            //else
            //{
            //    Repo.Add(CT3);
            //}

            AnotherTable AT = new AnotherTable();
            AT.AnotherTableID = 1;

            if (Repo.Exists<AnotherTable>(A => A.AnotherTableID == AT.AnotherTableID))
            {
                Repo.Update(AT);
            }
            else
            {
                Repo.Add(AT);
            }

            PT.AnotherID = AT.AnotherTableID;

            if (Repo.Exists<ParentTable>(P => P.ParentTableID == PT.ParentTableID))
            {
                Repo.Update(PT);
            }
            else
            {
                Repo.Add(PT);
            }
        }
    }

    class ParentTable
    {
        [SubSonicPrimaryKey]
        public int ParentTableID { get; set; }

        public string ParentValue { get; set; }

        public int AnotherID { get; set; }

        //public List<ChildTable> Children { get; set; }

        public ParentTable()
        {
            
        }
    }

    class ChildTable
    {
        [SubSonicPrimaryKey]
        public int ChildTableID { get; set; }

        public string Value { get; set; }

        public ChildTable()
        {

        }
    }

    class AnotherTable
    {
        public int AnotherTableID { get; set; }

        [SubSonicNullString]
        public string AnotherValue { get; set; }
    }
}
