using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public delegate void UpdateStackEvent();

public class ObservableStack<T> : Stack<T>
{
	public event UpdateStackEvent OnPush;
	public event UpdateStackEvent OnPop;
	public event UpdateStackEvent OnClear;

	// Creating a constructor to take in a stack of items that will occupie this stack
	public ObservableStack(ObservableStack<T> items) : base(items)
	{

	}

	// Creating an empty constructor to avoid errors when instanciating and ObservableStack without any items
	public ObservableStack()
	{

	}

	public new void Push(T item)
	{
		base.Push(item);

		if (OnPush != null)
		{
			OnPush();
		}
	}

	public new T Pop()
	{
		T item = base.Pop();

		if (OnPop != null)
		{
			OnPop();
		}
		return item;
	}

	public new void Clear()
	{
		base.Clear();

		if (OnClear != null)
		{
			OnClear();
		}
	}
}
