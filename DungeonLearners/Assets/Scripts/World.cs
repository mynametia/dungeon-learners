public class World {
    public string worldName;
    public string description;
    public string visibility;
    public User createdBy;
    // public World(){}

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