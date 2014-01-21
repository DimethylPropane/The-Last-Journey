using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Projet
{
    class ContentManagerGet
    {
        public static ContentManager _content;

        public static void Initialize(ContentManager toreplace)
        {
            _content = toreplace;
        }

        public static ContentManager Give()
        {
            return _content;
        }

    }
}
