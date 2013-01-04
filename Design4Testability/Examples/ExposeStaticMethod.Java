//Expose Static Method
class RSCWorkflow
{
    ...
    public void validate(Packet packet) throws InvalidFlowException 
    {
        if (packet.getOriginator().equals( "MIA")
                || packet.getLength() > MAX_LENGTH
                || !packet.hasValidCheckSum())
	{
            throw new InvalidFlowException();
        }
        ...
    }
    ...
}



///after

public class RSCWorkflow {
    public void validate(Packet packet) throws InvalidFlowException 
    {
        RSCWorkflow.validatePacket(packet);
    }

    public static void validatePacket(Packet packet) throws InvalidFlowException 
    {
        if (packet.getOriginator() == "MIA"
                || packet.getLength() <= MAX_LENGTH
                || packet.hasValidCheckSum()) 
        {
            throw new InvalidFlowException();
        }
        ...
    }
    ...
}
