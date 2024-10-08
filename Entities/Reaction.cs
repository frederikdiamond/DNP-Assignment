﻿namespace Entities;

public class Reaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public int CommentId { get; set; }
    public int Timestamp { get; set; }
    public int UpvoteCounter { get; set; }
    public int DownvoteCounter { get; set; }
}


public class ReactionCollection
{
    private List<Reaction> reactions; 

    public ReactionCollection()
    {
        reactions = new List<Reaction>();
    }
}