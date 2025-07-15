using System;

/// <summary>
/// Summary description for ListItem.
/// </summary>
public class ListItem
{
	private object m_objValue;
	private string m_strText;

	public ListItem(object ojbValue, string strText)
	{
		m_objValue = ojbValue;
		m_strText = strText;
	}

	public override string ToString()
	{
		return m_strText;
	}

	public object objValue
	{
		get
		{
			return m_objValue;
		}
	}

	public string strText
	{
		get
		{
			return m_strText;
		}
	}
}
