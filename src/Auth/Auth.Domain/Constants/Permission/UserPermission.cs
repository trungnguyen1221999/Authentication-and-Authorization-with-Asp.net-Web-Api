using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Constants.Permission
{
    public static class UserPermission
    {
        public static class Post
        {
            public const string Create = "Post.Create";
            public const string Edit = "Post.Edit";
            public const string Delete = "Post.Delete";
            public const string View = "Post.View";
        }

        public static class User
        {
            public const string Create = "User.Create";
            public const string Edit = "User.Edit";
            public const string Delete = "User.Delete";
            public const string View = "User.View";

        }
    }
}
