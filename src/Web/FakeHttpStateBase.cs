using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FakeN.Web
{
    /// <summary>
    /// Use <c>AllStates</c> to control the content
    /// </summary>
    public class FakeHttpStateBase : HttpApplicationStateBase
    {
        public FakeHttpStateBase()
        {
            AllStates = new Dictionary<string, object>();
        }

        public Dictionary<string, object> AllStates { get; set; }

        /// <summary>
        ///     When overridden in a derived class, gets the access keys for the objects in the collection.
        /// </summary>
        /// <returns>
        ///     An array of state object keys.
        /// </returns>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override string[] AllKeys
        {
            get { return AllStates.Keys.ToArray(); }
        }

        /// <summary>
        ///     Gets a <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> instance that
        ///     contains all the keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> instance that contains
        ///     all the keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.
        /// </returns>
        public override KeysCollection Keys
        {
            get
            {
                var col = new NameValueCollection();
                foreach (var kvp in AllStates)
                {
                    col.Add(kvp.Key, kvp.Value.ToString());
                }
                return col.Keys;
            }
        }

        /// <summary>
        ///     When overridden in a derived class, gets a reference to the <see cref="T:System.Web.HttpApplicationStateBase" />
        ///     object.
        /// </summary>
        /// <returns>
        ///     A reference to the <see cref="T:System.Web.HttpApplicationState" /> object.
        /// </returns>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override HttpApplicationStateBase Contents
        {
            get { return this; }
        }

        /// <summary>
        ///     When overridden in a derived class, gets the number of objects in the collection.
        /// </summary>
        /// <returns>
        ///     The number of objects in the collection.
        /// </returns>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override int Count
        {
            get { return AllStates.Count; }
        }

        /// <summary>
        ///     When overridden in a derived class, gets a state object by index.
        /// </summary>
        /// <returns>
        ///     The object referenced by <paramref name="index" />.
        /// </returns>
        /// <param name="index">The index of the object in the collection.</param>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override object this[int index]
        {
            get { return Get(index); }
        }

        /// <summary>
        ///     When overridden in a derived class, gets a state object by name.
        /// </summary>
        /// <returns>
        ///     The object referenced by <paramref name="name" />.
        /// </returns>
        /// <param name="name">The name of the object in the collection.</param>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override object this[string name]
        {
            get { return AllStates[name]; }
            set { AllStates[name] = value; }
        }

        /// <summary>
        ///     When overridden in a derived class, adds a new object to the collection.
        /// </summary>
        /// <param name="name">The name of the object to add to the collection.</param>
        /// <param name="value">The value of the object.</param>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override void Add(string name, object value)
        {
            AllStates.Add(name, value);
        }

        /// <summary>
        ///     When overridden in a derived class, removes all objects from the collection.
        /// </summary>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override void Clear()
        {
            AllStates.Clear();
        }

        /// <summary>
        ///     When overridden in a derived class, copies the elements of the collection to an array, starting at the specified
        ///     index in the array.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional array that is the destination for the elements that are copied from the
        ///     collection. The array must have zero-based indexing.
        /// </param>
        /// <param name="index">The zero-based index in <paramref name="array" /> at which to begin copying. </param>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override void CopyTo(Array array, int index)
        {
            var items = AllStates.Values.ToArray();
            Array.Copy(items, 0, array, index, items.Length);
        }

        /// <summary>
        ///     When overridden in a derived class, gets a state object by index.
        /// </summary>
        /// <returns>
        ///     The object referenced by <paramref name="index" />.
        /// </returns>
        /// <param name="index">The index of the application state object to get.</param>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override object Get(int index)
        {
            return AllStates.Values.ToArray()[index];
        }

        /// <summary>
        ///     When overridden in a derived class, gets a state object by name.
        /// </summary>
        /// <returns>
        ///     The object referenced by <paramref name="name" />.
        /// </returns>
        /// <param name="name">The name of the object to get.</param>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override object Get(string name)
        {
            return AllStates[name];
        }

        /// <summary>
        ///     When overridden in a derived class, returns an enumerator that can be used to iterate through the collection.
        /// </summary>
        /// <returns>
        ///     An object that can be used to iterate through the collection.
        /// </returns>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override IEnumerator GetEnumerator()
        {
            return AllStates.GetEnumerator();
        }

        /// <summary>
        ///     When overridden in a derived class, gets the name of a state object by index.
        /// </summary>
        /// <returns>
        ///     The name of the application state object.
        /// </returns>
        /// <param name="index">The index of the application state object to get.</param>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override string GetKey(int index)
        {
            return AllStates.Keys.ToArray()[index];
        }

        /// <summary>
        ///     Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to
        ///     serialize the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.
        /// </summary>
        /// <param name="info">
        ///     A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the
        ///     information required to serialize the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" />
        ///     instance.
        /// </param>
        /// <param name="context">
        ///     A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source
        ///     and destination of the serialized stream associated with the
        ///     <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="info" /> is null.</exception>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            AllStates.GetObjectData(info, context);
        }

        /// <summary>
        ///     When overridden in a derived class, locks access to objects in the collection in order to enable synchronized
        ///     access.
        /// </summary>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override void Lock()
        {
        }

        /// <summary>
        ///     When overridden in a derived class, removes the named object from the collection.
        /// </summary>
        /// <param name="name">The name of the object to remove from the collection.</param>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override void Remove(string name)
        {
            AllStates.Remove(name);
        }

        /// <summary>
        ///     When overridden in a derived class, removes all objects from the collection.
        /// </summary>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override void RemoveAll()
        {
            AllStates.Clear();
        }

        /// <summary>
        ///     When overridden in a derived class, removes a state object specified by index from the collection.
        /// </summary>
        /// <param name="index">The position in the collection of the item to remove.</param>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override void RemoveAt(int index)
        {
            var key = GetKey(index);
            AllStates.Remove(key);
        }

        /// <summary>
        ///     When overridden in a derived class, updates the value of an object in the collection.
        /// </summary>
        /// <param name="name">The name of the object to update.</param>
        /// <param name="value">The updated value of the object.</param>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override void Set(string name, object value)
        {
            AllStates[name] = value;
        }

        /// <summary>
        ///     When overridden in a derived class, unlocks access to objects in the collection to enable synchronized access.
        /// </summary>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override void UnLock()
        {
        }
    }
}