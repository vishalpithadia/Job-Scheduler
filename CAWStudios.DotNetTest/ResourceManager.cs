namespace CAWStudios.DotNetTest
{
    /// <summary>
    /// The resource manager is used to get the singleton resource and return it when the job is done using it.
    
    /// You are allowed to change this class  (modify existing methods, add new once etc)
    /// but the current logic should remain,as only 1 resource should ever be provided,
    /// if the GetResource method is called when the resource was already given to other, an exception must be thrown.
    /// </summary>
    internal static class ResourceManager
    {
        private static Resource? _resource = Resource.Self;
        
        
        public static Resource? GetResource()
        {
            if (_resource == null)
            {
                while(_resource == null) {
                    continue;
                }

                return GetResource();
            }
            var result = _resource;
            _resource = null;

            return result;
        }

        public static void ReturnResource(Resource resource)
        {
            _resource = resource;
        }
    }
}
