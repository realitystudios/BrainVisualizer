#!/usr/bin/env bash

set -x

echo "Testing for $TEST_PLATFORM"

${UNITY_EXECUTABLE:-xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' /opt/Unity/Editor/Unity} \
  -projectPath $(pwd) \
  -runTests \
  -testPlatform $TEST_PLATFORM \
  -testResults $(pwd)/$TEST_PLATFORM-results.xml \
  -logFile /dev/stdout \
  -batchmode

UNITY_EXIT_CODE=$?

if [ $UNITY_EXIT_CODE -eq 0 ]; then
  echo "Run succeeded, no failures occurred";
elif [ $UNITY_EXIT_CODE -eq 2 ]; then
  echo "Run succeeded, some tests failed";
elif [ $UNITY_EXIT_CODE -eq 3 ]; then
  echo "Run failure (other failure)";
else
  echo "Unexpected exit code $UNITY_EXIT_CODE";
fi

cat $(pwd)/$TEST_PLATFORM-results.xml | grep test-run | grep Passed

saxonb-xslt -s:$(pwd)/$TEST_PLATFORM-results.xml -xsl:./ci/nunit3-junit.xslt -o:$(pwd)/$TEST_PLATFORM-results-converted.xml

exit $UNITY_TEST_EXIT_CODE
