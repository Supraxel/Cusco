namespace Cusco.Dispatch;

/// <summary>
/// Abstract base type for all dispatch objects.
/// </summary>
public interface IDispatchObject
{
  // /// <summary>
  // /// An application defined context associated to the object.
  // /// </summary>
  // object context { get; set; }

  // /// <summary>
  // /// Activates the specified dispatch object.
  // /// </summary>
  // /// <remarks>
  // /// Dispatch objects such as queues and sources may be created in an inactive
  // /// state. Objects in this state have to be activated before any blocks
  // /// associated with them will be invoked.
  // /// <br/><br/>
  // /// The target queue of inactive objects can be changed using
  // /// dispatch_set_target_queue(). Change of target queue is no longer permitted
  // /// once an initially inactive object has been activated.
  // /// <br/><br/>
  // /// Calling <see cref="Activate"/> on an active object has no effect.
  // /// </remarks>
  // void Activate();

  // /// <summary>
  // /// Resumes the invocation of blocks on a dispatch object.
  // /// </summary>
  // /// <remarks>
  // /// Dispatch objects can be suspended with <see cref="Suspend"/>, which increments
  // /// an internal suspension count. <see cref="Resume"/> is the inverse operation,
  // /// and consumes suspension counts. When the last suspension count is consumed,
  // /// blocks associated with the object will be invoked again.
  // /// <br/><br/>
  // /// If the specified object has zero suspension count and is not an inactive
  // /// source, this function will result in an assertion and the process being
  // /// terminated.
  // /// </remarks>
  // void Resume();

  // // TODO: doc from https://github.com/apple/swift-corelibs-libdispatch/blob/main/dispatch/queue.h#L1049
  // void SetTargetQueue(DispatchQueue queue);

  // /// <summary>
  // /// Suspends the invocation of blocks on a dispatch object.
  // /// </summary>
  // /// <remarks>
  // /// A suspended object will not invoke any blocks associated with it. The
  // /// suspension of an object will occur after any running block associated with
  // /// the object completes.
  // /// <br/><br/>
  // /// Calls to <see cref="Suspend"/> must be balanced with calls
  // /// to <see cref="Resume"/>.
  // /// </remarks>
  // void Suspend();
}
