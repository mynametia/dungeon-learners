public class World {
    public string worldName;
    public string description;
    public string visibility;
    public int worldID;
    public User createdBy;

    public World(int worldID, string worldName, string description, string visibility){//, User createdBy) {
        this.worldID = worldID;
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