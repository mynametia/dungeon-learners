public class World {
    private string worldName;
    private string description;
    private string visibility;
    private User createdBy;
    public World(){}

    public World(string worldName, string description, string visibility){//, User createdBy) {
        this.worldName = worldName;
        this.description = description; 
        this.visibility = visibility;
        // this.createdBy = createdBy;
    }   

    public string getWorldName(){
        return worldName;
    }

    public string getDescription(){
        return description;
    }

    public User getCreatedBy(){
        return createdBy;
    }
}