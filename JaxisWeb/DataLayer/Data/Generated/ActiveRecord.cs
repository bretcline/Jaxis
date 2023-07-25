


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SubSonic.DataProviders;
using SubSonic.Extensions;
using System.Linq.Expressions;
using SubSonic.Schema;
using System.Collections;
using SubSonic;
using SubSonic.Repository;
using System.ComponentModel;
using System.Data.Common;

namespace Jaxis.LogicalModel.Data
{
    public static class ActiveRecordExtensions
    {
        public static T SingleOrDefault<T>( this IRepository<T> _repo, Expression<Func<T, bool>> expression ) where T : IActiveRecord
        {
            var results = _repo.Find( expression );
            T single = default( T );
            foreach ( T i in results )
            {
                single = i;
                single.SetIsLoaded( true );
                single.SetIsNew( false );
                break;
            }
            return single;
        }
    }
