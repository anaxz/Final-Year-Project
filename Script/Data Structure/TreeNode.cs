using System.Collections.Generic;
using UnityEngine;

// some help from: http://stackoverflow.com/questions/66893/tree-data-structure-in-c-sharp

public class TreeNode<T> {
	T data;
	List<TreeNode<T>> children;
	//TreeNode<T> root;

	public TreeNode(T data){
		this.data = data;
		children = new List<TreeNode<T>>();
		// each root/parent has own children
	}

	/*public T getCurrentData() {
		return root.data; 
	}*/

	public void addChild(T data){
		children.Add(new TreeNode<T>(data)); // add new data as a node
	}

	public TreeNode<T> getChild(T str){
		// iterate and find the node that is equal to str
		foreach (TreeNode<T> node in children){
			if (node.data.Equals(str)) return node;
		}
		return null;
	}

	public List<T> getChildData(){
		List<T> temp = new List<T>();
		foreach (TreeNode<T> node in children){
			temp.Add(node.data); // add the data to list
		}
		return temp;
	}

	// recursive -> from stackflow
	public void Traverse(TreeNode<T> node){
		foreach (TreeNode<T> child in node.children){
			Debug.Log(child.data.ToString());
			Traverse(child);
		}
	}

	public bool SearchNode(TreeNode<T> node){
		foreach (TreeNode<T> child in node.children){
			Debug.Log(child.data.ToString());
			if (child.data.Equals(node.data)) return true;
		}
		return false;
	}
}