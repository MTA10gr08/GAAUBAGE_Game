using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

namespace GAAUBAGE_Game.API.Networking
{
    public static class Endpoints
    {
        private const string BaseUrl = "https://localhost:7000";

        private static string GetAll(string resource) => $"{BaseUrl}/{resource}";
        private static string Create(string resource) => $"{BaseUrl}/{resource}";
        private static string Get(string resource, Guid id) => $"{BaseUrl}/{resource}/{id}";
        private static string Update(string resource, Guid id) => $"{BaseUrl}/{resource}/{id}";
        private static string Delete(string resource, Guid id) => $"{BaseUrl}/{resource}/{id}";
        private static string Next(string resource) => $"{BaseUrl}/{resource}/next";
        private static string Submit(string resource) => $"{BaseUrl}/{resource}/submit";

        public static class Users
        {
            private const string Resource = "users";
            public static string GetAll() => Endpoints.GetAll(Resource);
            public static string Post() => Endpoints.Create(Resource);
            public static string Get(Guid id) => Endpoints.Get(Resource, id);
            public static string Update(Guid id) => Endpoints.Update(Resource, id);
            public static string Delete(Guid id) => Endpoints.Delete(Resource, id);
        }
        public static class Images
        {
            private const string Resource = "images";
            public static string GetAll() => Endpoints.GetAll(Resource);
            public static string Create() => Endpoints.Create(Resource);
            public static string Get(Guid id) => Endpoints.Get(Resource, id);
            public static string Update(Guid id) => Endpoints.Update(Resource, id);
            public static string Delete(Guid id) => Endpoints.Delete(Resource, id);
        }
        public static class BackgroundClassification
        {
            private const string Resource = "backgroundclassifications";
            public static string GetAll() => Endpoints.GetAll(Resource);
            public static string Create() => Endpoints.Create(Resource);
            public static string Get(Guid id) => Endpoints.Get(Resource, id);
            public static string Update(Guid id) => Endpoints.Update(Resource, id);
            public static string Delete(Guid id) => Endpoints.Delete(Resource, id);
            public static string Next() => Endpoints.Next(Resource);
            public static string Submit() => Endpoints.Submit(Resource);
        }
        public static class ContextClassification
        {
            private const string Resource = "contextclassifications";
            public static string GetAll() => Endpoints.GetAll(Resource);
            public static string Create() => Endpoints.Create(Resource);
            public static string Get(Guid id) => Endpoints.Get(Resource, id);
            public static string Update(Guid id) => Endpoints.Update(Resource, id);
            public static string Delete(Guid id) => Endpoints.Delete(Resource, id);
            public static string Next() => Endpoints.Next(Resource);
            public static string Submit() => Endpoints.Submit(Resource);
        }
        public static class TrashCount
        {
            private const string Resource = "trashcounts";
            public static string GetAll() => Endpoints.GetAll(Resource);
            public static string Create() => Endpoints.Create(Resource);
            public static string Get(Guid id) => Endpoints.Get(Resource, id);
            public static string Update(Guid id) => Endpoints.Update(Resource, id);
            public static string Delete(Guid id) => Endpoints.Delete(Resource, id);
            public static string Next() => Endpoints.Next(Resource);
            public static string Submit() => Endpoints.Submit(Resource);
        }
        public static class TrashBoundingBox
        {
            private const string Resource = "trashboundingboxes";
            public static string GetAll() => Endpoints.GetAll(Resource);
            public static string Create() => Endpoints.Create(Resource);
            public static string Get(Guid id) => Endpoints.Get(Resource, id);
            public static string Update(Guid id) => Endpoints.Update(Resource, id);
            public static string Delete(Guid id) => Endpoints.Delete(Resource, id);
            public static string Next() => Endpoints.Next(Resource);
            public static string Submit() => Endpoints.Submit(Resource);
        }
        public static class TrashSuperCategory
        {
            private const string Resource = "trashsupercategories";
            public static string GetAll() => Endpoints.GetAll(Resource);
            public static string Create() => Endpoints.Create(Resource);
            public static string Get(Guid id) => Endpoints.Get(Resource, id);
            public static string Update(Guid id) => Endpoints.Update(Resource, id);
            public static string Delete(Guid id) => Endpoints.Delete(Resource, id);
            public static string Next() => Endpoints.Next(Resource);
            public static string Submit() => Endpoints.Submit(Resource);
        }
        public static class TrashCategory
        {
            private const string Resource = "trashcategory";
            public static string GetAll() => Endpoints.GetAll(Resource);
            public static string Create() => Endpoints.Create(Resource);
            public static string Get(Guid id) => Endpoints.Get(Resource, id);
            public static string Update(Guid id) => Endpoints.Update(Resource, id);
            public static string Delete(Guid id) => Endpoints.Delete(Resource, id);
            public static string Next() => Endpoints.Next(Resource);
            public static string Submit() => Endpoints.Submit(Resource);
        }
        public static class Segmentation
        {
            private const string Resource = "Segmentations";
            public static string GetAll() => Endpoints.GetAll(Resource);
            public static string Create() => Endpoints.Create(Resource);
            public static string Get(Guid id) => Endpoints.Get(Resource, id);
            public static string Update(Guid id) => Endpoints.Update(Resource, id);
            public static string Delete(Guid id) => Endpoints.Delete(Resource, id);
            public static string Next() => Endpoints.Next(Resource);
            public static string Submit() => Endpoints.Submit(Resource);
        }
    }
}
