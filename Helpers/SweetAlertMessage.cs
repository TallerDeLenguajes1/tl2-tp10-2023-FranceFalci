namespace tl2_tp10_2023_FranceFalci;
public class SweetAlertMessage
{
  public string Title { get; set; }
  public string Message { get; set; }
  public SweetAlertMessageType Type { get; set; }
}

public enum SweetAlertMessageType
{
  Success,
  Error,
  Warning,
  Info
}