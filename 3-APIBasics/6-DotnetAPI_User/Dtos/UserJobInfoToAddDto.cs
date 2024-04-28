namespace DotnetAPI.Dtos 
{
    public partial class UserJobInfoToAddDto
    {
        public string jobTitle { get; set; }
        public string department { get; set; }

        public UserJobInfoToAddDto()
        {
            if( jobTitle == null )
            {
                jobTitle = "";
            }

            if( department == null)
            {
                department = "";
            }
        }
    }
}