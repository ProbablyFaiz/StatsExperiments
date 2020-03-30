# Created by: faiz
# Created on: 3/19/20
library(broom)

args <- commandArgs(trailingOnly=TRUE)
# csvFile <- args[1]
# formula <- args[2]
csvFile <- "StatsExperiments/ConversionChart.csv"
formula <- "formula = Percentile ~ ScaledScore"
regData <- read.csv(file = csvFile, fileEncoding="UTF-8-BOM")

lReg <- lm(as.formula(formula), data=regData)
topLineInfo <- glance(lReg)
# Temp header
cat("r-squared", "adjusted-r-squared", "f-statistic", "f-statistic-p-value", "rmse", "median-residual", sep=",")
cat("\n")

RMSE <- sqrt(crossprod(lReg$residuals) / length(lReg$residuals))

cat(topLineInfo$r.squared, topLineInfo$adj.r.squared, topLineInfo$statistic, topLineInfo$p.value, RMSE, median(lReg$residuals), sep=",")
cat("\n\n")
coeffSummary <- tidy(lReg)
# Temp header
cat("Term", "Coefficient", "P-value", "t-statistic", "Standard Error", sep=",")
cat("\n")
for (row in seq_len(nrow(coeffSummary))) {
  cat(coeffSummary$term[row], coeffSummary$estimate[row], coeffSummary$p.value[row], coeffSummary$statistic[row], coeffSummary$std.error[row], sep=",")
  cat("\n")
}
