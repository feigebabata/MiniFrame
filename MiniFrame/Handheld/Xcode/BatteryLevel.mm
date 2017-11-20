
extern "C" {
	float _GetBatteryLevel () {
		BOOL orig = [UIDevice currentDevice].batteryMonitoringEnabled;
		[UIDevice currentDevice].batteryMonitoringEnabled = YES;
		return [UIDevice currentDevice].batteryLevel;
		[UIDevice currentDevice].batteryMonitoringEnabled = orig;
	}
}